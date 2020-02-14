using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Infrastructure.AutoMapperConfig
{
    public class DeviceProfile: AutoMapper.Profile
    {
        public DeviceProfile()
        {
            CreateMap<Models.CreateDeviceModel, Application.Commands.Device.CreateDeviceCommand>();
            CreateMap<Common.ResponseModel, Models.LogicModel>();

            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Device, Application.Queries.DeviceModel>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Channel, Application.Queries.Channel>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Controller, Application.Queries.Controller>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Part, Application.Queries.Part>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Sensor, Application.Queries.Sensor>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.DeviceFunction, Application.Queries.DeviceFunction>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.DeviceAlarm, Application.Queries.DeviceAlarm>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Location, Application.Queries.Location>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Moment, Application.Queries.Moment>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.TimedTask, Application.Queries.TimedTask>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Terminal, Application.Queries.TerminalDevice>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Load, Application.Queries.Load>();
            CreateMap<Device.Domain.AggregatesModel.DeviceAggregate.Camera, Application.Queries.Camera>();

            CreateMap<Device.Domain.AggregatesModel.RegionAggregate.Region, Application.Queries.RegionModel>();
            CreateMap<Device.Domain.AggregatesModel.BrandAggregate.Brand, Application.Queries.BrandViewModel>();


            CreateMap<Common.ConfigModel.SkynetTerminal.Models.ChannelStatusResultDto, Application.Commands.Device.FreshSwitchStatusCommand>();
            CreateMap<Common.ConfigModel.SkynetTerminal.Models.DeviceAlarmResultDto, Application.Commands.Device.FreshAlarmStatusCommand>();
            CreateMap<Common.CustomResultDto<Common.Commands.SkynetTerminalCmdEnum>, Application.Commands.Device.FreshTerminalConnectionCommand>();
        }
    }
}
