using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using SFBR.Device.Api.Application.Commands.Device;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Domain.AggregatesModel.BrandAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate;
using SFBR.Device.Domain.AggregatesModel.RegionAggregate;
using SFBR.Device.Domain.AggregatesModel.UserAggregate;
using SFBR.Device.Infrastructure.Repositories;
using SFBR.EventBus.Abstractions;

namespace SFBR.Device.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string qconstr)
        {
            QueriesConnectionString = qconstr;

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DeviceQueries(new SqlConnection(QueriesConnectionString))).As<IDeviceQueries>().InstancePerDependency();
            builder.Register(c => new BrandQueries(new SqlConnection(QueriesConnectionString))).As<IBrandQueries>().InstancePerDependency();
            builder.Register(c => new RegionQueries(new SqlConnection(QueriesConnectionString))).As<IRegionQueries>().InstancePerDependency();
            builder.Register(c => new TerminalQueries(new SqlConnection(QueriesConnectionString))).As<ITerminalQueries>().InstancePerDependency();
            //builder.Register(c => new DeviceTypeQueries(QueriesConnectionString)).As<IDeviceTypeQueries>().InstancePerLifetimeScope();

            builder.RegisterType<BrandRepository>().As<IBrandRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceRepository>().As<IDeviceRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DeviceTypeRepository>().As<IDeviceTypeRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(CreateDeviceCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
