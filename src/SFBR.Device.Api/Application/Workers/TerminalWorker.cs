using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SFBR.Device.Api.Application.Commands.Device;
using SFBR.Device.Api.Application.Queries;
using SFBR.Device.Common;
using SFBR.Device.Common.Commands;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Models;
using SFBR.Device.Common.Tool;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace SFBR.Device.Api.Application.Workers
{
    public class TerminalWorker: IWorker<BaseResultDto<SkynetTerminalCmdEnum>>
    {
        private readonly static MemoryCache memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        /// <summary>
        /// 
        /// </summary>
        private readonly static BufferBlock<BaseResultDto<SkynetTerminalCmdEnum>> statusWorker = new BufferBlock<BaseResultDto<SkynetTerminalCmdEnum>>();
       

        private static IServiceProvider _services;
        private static IMapper _mapper;

        static TerminalWorker()
        {
            Instance = new TerminalWorker();
           
            Task.Run(async () =>
            {
                while (true)
                {
                    var item = await statusWorker.ReceiveAsync();
                    try
                    {
                        if(item is DeviceAlarmResultDto || item is ChannelStatusResultDto)
                        {
                            await CheckAndSaveAsync(item, SaveStatusStatusAsync);
                        }
                        else if(item is CustomResultDto<SkynetTerminalCmdEnum>)
                        {
                            await SaveCustomEventAsync(item as CustomResultDto<SkynetTerminalCmdEnum>);
                        }
                        else
                        {
                            await SaveConfigDataAsync(item);
                        }
                    }
                    catch { }
                }
            });
        }
        private TerminalWorker()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public static TerminalWorker Instance { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="services"></param>
        public void Execute(BaseResultDto<SkynetTerminalCmdEnum> value, IServiceProvider services)
        {
            if (_services == null) _services = services;
            if (_mapper == null) _mapper = _services.GetService<IMapper>();
            statusWorker.Post(value);
        }

        private static async Task CheckAndSaveAsync<T>(T item,Func<T,Task> func) where T : BaseResultDto<SkynetTerminalCmdEnum>
        {
            string newValue = item.Data.ToStr();
            var key = $"{item.GetType().Name}{item.UniqueId}";
            
            if (memoryCache.TryGetValue(key, out string value))
            {
                if (!value.Equals(newValue))
                {
                    await func(item);
                }
            }
            else
            {
                memoryCache.Set(key, newValue, TimeSpan.FromSeconds(120));
                await func(item);
            }
        }

        private static async Task SaveStatusStatusAsync(BaseResultDto<SkynetTerminalCmdEnum> item)
        {
            var _mediator = _services.GetService<IMediator>();
            using (var query = _services.GetService<IDeviceQueries>())
            {
                if (await query.Exists(item.UniqueId))
                {
                    if (item is DeviceAlarmResultDto)
                    {
                        await _mediator.Send(_mapper.Map<FreshAlarmStatusCommand>(item));
                    }
                    else if (item is ChannelStatusResultDto)
                    {
                        await _mediator.Send(_mapper.Map<FreshSwitchStatusCommand>(item));
                    }
                }
                else
                {
                    await AddDevice(new CreateDeviceCommand
                    {
                        EquipNum = item.UniqueId,
                        DeviceName = "智维终端" + item.UniqueId,
                        DeviceIP = item.FromIP,
                        DevicePort = item.FromPort,
                        DeviceTypeCode = nameof(Terminal),
                        ModelCode = $"{Convert.ToChar(item.Data[4])}.{Convert.ToChar(item.Data[5])}",
                        Enabled = true,
                        Description = "系统自动分配，请分配区域和归属"
                    });
                } 
            }
        }


        private static async Task SaveCustomEventAsync(CustomResultDto<SkynetTerminalCmdEnum> item)
        {
            var _mediator = _services.GetService<IMediator>();
            await _mediator.Send(_mapper.Map<FreshTerminalConnectionCommand>(item));
        }

        private static async Task AddDevice(CreateDeviceCommand command)
        {
            using (var query = _services.GetService<IDeviceQueries>())
            {
                if (await query.Exists(command.EquipNum)) return;
                var _mediator = _services.GetService<IMediator>();
                await _mediator.Send(command); 
            }
        }

        private static async Task SaveConfigDataAsync(BaseResultDto<SkynetTerminalCmdEnum> value)
        {
            var _mediator = _services.GetService<IMediator>();
            await _mediator.Send(new FreshConfigCommand(value));
        }
    }
}
