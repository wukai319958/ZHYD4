using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using SFBR.Device.Api.Application.IntegrationEvents;
using SFBR.Device.Api.Application.Workers;
using SFBR.Device.Api.Infrastructure.ApiGateway;
using SFBR.Device.Api.Infrastructure.AutoMapperConfig;
using SFBR.Device.Api.Infrastructure.Filters;
using SFBR.Device.Api.Infrastructure.Services;
using SFBR.Device.Api.Models;
using SFBR.Device.Common.Commands;
using SFBR.Device.Common.Interface;
using SFBR.Device.Infrastructure;
using SFBR.EventBus;
using SFBR.EventBus.Abstractions;
using SFBR.EventBusRabbitMQ;
using SFBR.IntegrationEventLogEF;
using SFBR.IntegrationEventLogEF.Services;
using SFBR_Client.SkynetTerminal;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFBR.Device.Api
{
    static class CustomExtensionsMethods
    {
        #region 服务配置
        /// <summary>
        /// 安全密钥
        /// </summary>
        private const string secretKey = "72A21C1C5C84C46-ACD3_!@#$%^&*(()$WEQ<>:|``~fjdkladjfkla37298vhsd";

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); })
                .AddDataAnnotationsLocalization()
                .AddControllersAsServices();  //Injecting Controllers themselves thru DI
                                              //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    //.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<DeviceContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                           });
                   },
                       ServiceLifetime.Scoped
                   );
            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });         
            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IDeviceIntegrationEventService, DeviceIntegrationEventService>();

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                var factory = new ConnectionFactory()
                {
                    HostName = configuration["EventBusConnection"],
                    DispatchConsumersAsync = true
                };

                if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                {
                    factory.UserName = configuration["EventBusUserName"];
                }

                if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                {
                    factory.Password = configuration["EventBusPassword"];
                }

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }
                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                }

                return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                         new Info()
                         {
                             Title = $"Device HTTP API v{description.ApiVersion}",
                             Version = description.ApiVersion.ToString(),
                             Description = "切换版本请点右上角版本切换",
                             TermsOfService = "Terms Of Service",
                             Contact = new Contact() { Name = "四方博瑞", Email = "",Url= "http://www.hz-sfbr.com/" }
                         }
                    );
                }

                options.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 在下方输入Bearer { token } 即可，注意两者之间有空格",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{typeof(SFBR.Device.Domain.SeedWork.IAggregateRoot).Assembly.GetName().Name}.xml"));
                //var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                //options.AddSecurityRequirement(security);

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<DeviceSettings>(configuration);
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options =>
            {
                //options.Authority = identityUrl;//过期后获取token的地址，由于目前未采用过期策略可以忽略
                //options.RequireHttpsMetadata = false;
                //options.Audience = "device";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,//是否验证Issuer
                    ValidateAudience = false,//是否验证Audience
                    ValidateLifetime = false,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    //ValidAudience = config["JwtOption:Audience"],//Audience
                    //ValidIssuer = config["JwtOption:Issuer"],//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"] ?? secretKey))//拿到SecurityKey
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(option =>
            {
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
                option.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader("api-version"));
            }).AddMvcCore();
            services.AddVersionedApiExplorer(option =>
            {
                option.GroupNameFormat = "'v'VVV";
                option.AssumeDefaultVersionWhenUnspecified = true;

            });
            return services;
        }

        public static IServiceCollection AddCustomLocalization(this IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources")
                .Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                    {
                        new CultureInfo("zh-CN"),
                        new CultureInfo("en-US")
                    };

                options.DefaultRequestCulture = new RequestCulture("zh-CN");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            return services;
        }

        public static IServiceCollection AddCustomAutoMpper(this IServiceCollection services)
        {
            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(builder =>
            {
                builder.AddProfile<DeviceProfile>();
            });
            var mapper = config.CreateMapper();
            return services.AddSingleton(mapper);
        }

        public static IServiceCollection AddTerminal(this IServiceCollection services,IConfiguration configuration)
        {
            var terminal = new SkynetTerminalClient(
                new Common.MsgServerInfo { HostIP= configuration["EventBusConnection"]??"127.0.0.1" , Password= configuration["EventBusPassword"]??"123456", UserName= configuration["EventBusUserName"]??"sfbr" },
                new Common.MsgServerInfo { HostIP = configuration["EventBusConnection"] ?? "127.0.0.1", Password = configuration["EventBusPassword"] ?? "123456", UserName = configuration["EventBusUserName"] ?? "sfbr" });
            return services.AddSingleton<ISkynetTerminalClient>(terminal);
        }
        public static IServiceCollection AddMqttClient(this IServiceCollection services, IConfiguration configuration)
        {
            var factory = new MqttFactory();
            var mqttClient = factory.CreateMqttClient();
            var clientOptions = new MqttClientOptions
            {
                Credentials = new MqttClientCredentials
                {
                    Username = configuration["mqtt:username"] ?? "sfbr",
                    Password = Encoding.UTF8.GetBytes((configuration["mqtt:password"] ?? "123456"))
                },
                ClientId = Program.AppName,
                ChannelOptions = new MqttClientTcpOptions
                {
                    Server = configuration["mqtt:server"] ?? "127.0.0.1",
                    Port = int.Parse(configuration["mqtt:port"] ?? "1883")
                }

            };

            mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(async e =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromSeconds(10));//每隔10秒重新发起连接
                    try
                    {
                        var result = await mqttClient.ConnectAsync(clientOptions);
                        if (result.ResultCode == MqttClientConnectResultCode.Success)
                        {
                            break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("### RECONNECTING FAILED ###");
                    }
                }
            });
            try
            {
                var result = mqttClient.ConnectAsync(clientOptions).Result;
            }
            catch
            {

            }
            return services.AddSingleton(mqttClient);
        }

        #endregion

        #region 中间件
        /// <summary>
        /// 自定义文档
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="host">服务地址</param>
        /// <returns></returns>
        public static IApplicationBuilder MapCustomDoc(this IApplicationBuilder app)
        {
            return app.Map("/doc/push",builder=> 
            {
                builder.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("未找到相应的文档");
                });
            });
        }

        #endregion


        #region 摄像机视频流获取
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> process = new System.Collections.Concurrent.ConcurrentDictionary<string, string>();

        public static string GetCameraVedioUrl(string username, string password,string ip,int port=554, string localIP = "127.0.0.1", string vedioResalution = "640*480")
        {
            string url = $"rtsp://{username}:{password}@{ip}:{554}/h264/ch1/main/av_stream";            
            var index = url.IndexOf(@"//");
            string name = url.Substring(index + 2);
            return GetUrl(url, name,localIP,vedioResalution);
        }

        public static string GetCameraVedioUrl(string sourceUrl, string localIP = "127.0.0.1", string vedioResalution = "640*480")
        {
            string url = sourceUrl;
            var index = url.IndexOf(@"//");
            string name = url.Substring(index + 2);
            return GetUrl(url, name, localIP, vedioResalution);
        }


        static string GetUrl(string url, string name,string localIP = "127.0.0.1",string vedioResalution = "640*480")
        {
            string outputUrl;
            if (process.TryGetValue(name, out outputUrl))
            {
                return outputUrl;
            }

            string id = Guid.NewGuid().ToString();
            outputUrl = $"rtmp://{localIP}/mylive/" + name;
            //第三步检测服务器SRS是否正常开启
            string strpath = System.IO.Directory.GetCurrentDirectory(); //文件名
            string fileName = @"" + strpath + "\\ffmpeg\\bin\\ffmpeg.exe";//ffmpeg的绝对路径可以自由更改
            string arguments = " -i " + "\"" + url + "\"" + " -vcodec copy -acodec aac -ar 44100 -strict -2 -ac 1 -f flv -s " + vedioResalution + " \"" + outputUrl + "\"";
            //arguments = "-i " + url + "  -f flv -r 25 -s " + VedioResalution + " -an " + "\"" + outputUrl + "\"";//ffmpeg参数
            dynamic obj = new { name = fileName, arguments = arguments };
            System.Threading.Thread th = new System.Threading.Thread(new ParameterizedThreadStart(ProcessStart));
            th.IsBackground = true;
            th.Start(obj);
            process.TryAdd(name, outputUrl);
            return outputUrl;
        }


        static void ProcessStart(dynamic obj)
        {
            string name = obj.name;
            string arguments = obj.arguments;
            Process p = new Process();//创建进程
            p.StartInfo.FileName = name;//ffmpeg的绝对路径可以自由更改
            //p.StartInfo.Arguments = "-i " + url + "  -f flv -r 25 -s 1280*720 -an " + "\"" + outputUrl + "\"";//ffmpeg参数
            p.StartInfo.Arguments = arguments;


            p.StartInfo.UseShellExecute = false;//不使用操作系统外壳程序启动线程
            p.StartInfo.RedirectStandardError = true;//把外部程序错误输出写到StandardError流中
            p.StartInfo.CreateNoWindow = true; //设置置不显示示窗口

            p.Disposed += P_Disposed;
            p.Start();//启动线程
            p.BeginErrorReadLine();//开始异步读取
            p.WaitForExit();//阻塞等待进程结束
            p.Close();//关闭进程
            p.Dispose();//释放资源
        }

        static void P_Disposed(object sender, EventArgs e)
        {
            Process p = (Process)sender;
            dynamic obj = new { name = p.StartInfo.FileName, arguments = p.StartInfo.Arguments };
            ProcessStart(obj);
        }


        #endregion

       
        public static string ToJson<T>(this T data) where T : class
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
    }
}
