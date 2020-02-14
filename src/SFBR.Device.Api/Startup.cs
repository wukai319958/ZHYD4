using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SFBR.Device.Api.Application.IntegrationEvents.Events;
using SFBR.Device.Api.Infrastructure.AutofacModules;
using SFBR.EventBus.Abstractions;

namespace SFBR.Device.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services
                .AddCustomMvc()
                .AddCustomDbContext(Configuration)
                .AddCustomSwagger(Configuration)
                .AddCustomIntegrations(Configuration)
                .AddCustomConfiguration(Configuration)
                .AddEventBus(Configuration)
                .AddCustomAuthentication(Configuration)
                .AddCustomApiVersion()
                .AddCustomLocalization()
                .AddTerminal(Configuration)
                .AddMqttClient(Configuration)
                .AddCustomAutoMpper();
            

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            var supportedCultures = new[]
            {
                new CultureInfo("zh-CN"),
                new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseApiGateWay("alarm");//注册警报服务网关
            app.MapCustomDoc();

            app.UseMvcWithDefaultRoute();

            app.UseRequestLocalization();

            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  foreach (var description in provider.ApiVersionDescriptions)
                  {
                      c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                  }
                  //c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
              });

            ConfigureEventBus(app);
        }
        /// <summary>
        /// 订阅外部事件
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //string json = "{\"DeviceId\":\"0702edfe-93c9-41f2-a899-01eaf20ad051\",\"DeviceName\":\"智维终端00000\",\"DeviceTypeCode\":\"Terminal\",\"ModelCode\":\"1.0\",\"EquipNum\":\"00000\",\"TargetCode\":\"Sensor_3\",\"AlarmCode\":\"Terminal_1.0_32\",\"ClearTime\":\"2019-10-23T11:41:32.9001049Z\",\"ClearReason\":1,\"Description\":\"编号00000的站点温度恢复正常\",\"Id\":\"4b06d188-f751-42de-b991-7360f1b5ea7b\",\"CreationDate\":\"2019-10-23T11:41:32.9001045Z\"}";// {"DeviceId":"0702edfe - 93c9 - 41f2 - a899 - 01eaf20ad051","DeviceName":"智维终端00000","DeviceTypeCode":"Terminal","ModelCode":"1.0","EquipNum":"00000","TargetCode":"Sensor_3","AlarmCode":"Terminal_1.0_32","ClearTime":"2019 - 10 - 23T11: 41:32.9001049Z","ClearReason":1,"Description":"编号00000的站点温度恢复正常","Id":"4b06d188 - f751 - 42de - b991 - 7360f1b5ea7b","CreationDate":"2019 - 10 - 23T11: 41:32.9001045Z"}"
            //eventBus.Publish(Newtonsoft.Json.JsonConvert.DeserializeObject<TerminalClearAlarmIntegrationEvent>(json));
            //eventBus.Subscribe<TerminalClearAlarmIntegrationEvent, IIntegrationEventHandler<TerminalClearAlarmIntegrationEvent>>();//订阅设备上线事件，消息由网关推送
            //TODO:其他事件
        }
    }
}
