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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet.Client;
using SFBR.EventBus.Abstractions;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.IntegrationEvents.EventHandling;
using SFBR.Log.Api.IntegrationEvents.Events;

namespace SFBR.Log.Api
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
                .AddMqttClient(Configuration)
                .AddCustomAutoMpper();


            var container = new ContainerBuilder();
            container.Populate(services);

            //container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            ConfigureMqtt(app);
        }
        /// <summary>
        /// 订阅外部事件
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TerminalRaiseAlarmIntegrationEvent, TerminalRaiseAlarmIntegrationEventHandler>();//发生警报
            eventBus.Subscribe<TerminalClearAlarmIntegrationEvent, TerminalClearAlarmIntegrationEventHandler>();//解除警报
            //TODO:其他事件
        }

        private void ConfigureMqtt(IApplicationBuilder app)
        {
            var mqttClient = app.ApplicationServices.GetRequiredService<IMqttClient>();
            mqttClient.SubscribeAsync("log/#");
        }
    }
}
