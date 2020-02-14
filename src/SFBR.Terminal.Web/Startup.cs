using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SFBR.Terminal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddHttpClient();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseDefaultPage();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.MapApiGateWay("device", 5200, Configuration["device"])
                .MapApiGateWay("terminal", 5200, Configuration["terminal"])
                //.MapApiGateWay("account", 5200, Configuration["account"])
                .MapApiGateWay("region", 5200, Configuration["region"])
                .MapApiGateWay("brand", 5200, Configuration["brand"])
                .MapApiGateWay("alarm", 5201, Configuration["alarm"])
                .MapApiGateWay("log", 5201, Configuration["log"])
                .MapApiGateWay("data", 5202, Configuration["data"])
                .MapSwaggerGateWay("device", 5200, Configuration["device"])
                .MapSwaggerGateWay("terminal", 5200, Configuration["terminal"])
                .MapSwaggerGateWay("region", 5200, Configuration["region"])
                .MapSwaggerGateWay("brand", 5200, Configuration["brand"])
                //.MapSwaggerGateWay("account", 5200, Configuration["account"])
                .MapSwaggerGateWay("alarm", 5201, Configuration["alarm"])
                .MapSwaggerGateWay("log", 5201, Configuration["log"])
                .MapSwaggerGateWay("data", 5202, Configuration["data"]);
        }
    }

    static class CustomExt
    {
        #region 中间件
        /// <summary>
        /// 网关
        /// </summary>
        /// <param name="app"></param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="host">服务地址</param>
        /// <returns></returns>
        public static IApplicationBuilder MapApiGateWay(this IApplicationBuilder app, string serviceName, int port, string ip = null)
        {
            return app.Map($"/api/{serviceName}", builder =>
            {
                builder.Use(async (context, next) =>
                {
                    string host = string.IsNullOrEmpty(ip) ? context.Request.Host.Host : ip;
                    try
                    {
                        string url = $"{context.Request.Scheme}://{host}:{port}{context.Request.Path.Value}{context.Request.QueryString}";
                        var request = new HttpRequestMessage(new HttpMethod(context.Request.Method), url);
                        if (context.Request.Body.CanSeek) context.Request.Body.Seek(0, SeekOrigin.Begin);
                        request.Content = new StreamContent(context.Request.Body);
                        if (context.Request.Headers != null)
                        {
                            foreach (var item in context.Request.Headers)
                            {
                                if (item.Key.StartsWith("content", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    request.Content.Headers.Add(item.Key, item.Value.ToArray());

                                }
                                else
                                {
                                    request.Headers.Add(item.Key, item.Value.ToArray());
                                }
                            }
                        }
                        var factory = app.ApplicationServices.GetService<IHttpClientFactory>();
                        using (var httpClient = factory.CreateClient())
                        {
                            var result = await httpClient.SendAsync(request);
                            context.Response.StatusCode = (int)result.StatusCode;
                            var data = await result.Content?.ReadAsByteArrayAsync();
                            if (result.Content != null && result.Content.Headers != null)
                            {
                                foreach (var item in result.Content.Headers)
                                {
                                    context.Response.Headers.Add(item.Key, new Microsoft.Extensions.Primitives.StringValues(item.Value.ToArray()));
                                }
                            }
                            await context.Response.Body.WriteAsync(data, 0, data.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(ex.ToString());
                    }
                });
            });
        }

        public static IApplicationBuilder MapSwaggerGateWay(this IApplicationBuilder app, string serviceName, int port, string ip = null)
        {
            return app.Map($"/{serviceName}/swagger", builder =>
            {
                builder.Use(async (context, next) =>
                {
                    string host = string.IsNullOrEmpty(ip) ? context.Request.Host.Host : ip;
                    try
                    {
                        string url = $"{context.Request.Scheme}://{host}:{port}/swagger";
                        context.Response.Redirect(url, true);
                    }
                    catch (Exception ex)
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync(ex.ToString());
                    }
                });
            });
        }

        public static IApplicationBuilder UseDefaultPage(this IApplicationBuilder app)
        {
            return app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api"))
                {
                    context.Request.Path = "/index.html";
                    context.Response.StatusCode = 200;
                    await next();
                }
            });
        }
        #endregion
    }
}
