using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using SFBR.Data.Api.Infrastructure;
using SFBR.Data.Api.ViewModel;
using SFBR.Device.Common;
using SFBR.Device.Common.Commands;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Models;
using SFBR.Device.Common.ConfigModel.SkynetTerminal.Results;
using SFBR.Device.Common.Tool;
using SFBR_Client.SkynetTerminal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api
{
    public class TerminalConsumer
    {
        internal static readonly ConcurrentDictionary<string, DeviceActor> valuePairs = new ConcurrentDictionary<string, DeviceActor>();
        private readonly SkynetTerminalClient _client;
        private readonly IMqttClient _mqttClient;
        private IConfiguration _configuration;
        private IServiceProvider _serviceProvider;

        public static ConcurrentDictionary<string, DeviceActor> RealDataCaches => valuePairs;

        public TerminalConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            //_client = new SkynetTerminalClient(new MsgServerInfo() { HostIP = "192.168.0.251", UserName = "sfbr", Password = "123456" }, new MsgServerInfo() { HostIP = "192.168.0.251", UserName = "sfbr", Password = "123456" });
            _client = new SkynetTerminalClient(
                new MsgServerInfo { HostIP = configuration["EventBusConnection"], Password = configuration["EventBusPassword"], UserName = configuration["EventBusUserName"] },
                new MsgServerInfo { HostIP = configuration["EventBusConnection"], Password = configuration["EventBusPassword"], UserName = configuration["EventBusUserName"] });
            _client.MessageCallBack += Client_MessageCallBack;
            _client.Subscribe("#.SkynetTerminal.*");//订阅所有消息
            _mqttClient = serviceProvider.GetService<IMqttClient>();
        }

        private async void Client_MessageCallBack(string topic, BaseResultDto<SkynetTerminalCmdEnum> data)
        {
            await Save(data);
        }

        private async Task Save(BaseResultDto<SkynetTerminalCmdEnum> data)
        {
            if (data == null) return ;
            if (!valuePairs.TryGetValue(data.UniqueId, out DeviceActor deviceActor))
            {
                deviceActor = new DeviceActor();
                valuePairs.TryAdd(data.UniqueId, deviceActor);
            };
            deviceActor.LastUpdate = DateTime.UtcNow;
            MqttApplicationMessage message = null;
            if (data is AnalogQuantityResultDto)//模拟量
            {
                using (var db = _serviceProvider.GetService<DataContext>())
                {
                    var temp = data as AnalogQuantityResultDto;
                    if (!temp.Voltage.Equals(deviceActor.Voltage))
                    {
                        db.Voltages.Add(new Model.Voltage
                        {
                            EquipNum = temp.UniqueId,
                            Value = temp.Voltage,
                            Position = "-1"
                        });
                        deviceActor.Voltage = temp.Voltage;
                    }
                    if (!temp.Current.Equals(deviceActor.Current))
                    {
                        db.Currents.Add(new Model.Current
                        {
                            EquipNum = temp.UniqueId,
                            Value = temp.Current,
                            Position = "-1"
                        });
                        deviceActor.Current = temp.Current;
                    }
                    if (!temp.Temperature.Equals(deviceActor.Temperature))
                    {
                        db.Temperatures.Add(new Model.Temperature
                        {
                            EquipNum = temp.UniqueId,
                            Value = temp.Temperature,
                            Position = "-1"
                        });
                        deviceActor.Temperature = temp.Temperature;
                    }
                    if (!temp.Humidity.Equals(deviceActor.Humidity))
                    {
                        db.Humidities.Add(new Model.Humidity
                        {
                            EquipNum = temp.UniqueId,
                            Value = temp.Humidity,
                            Position = "-1"
                        });
                        deviceActor.Humidity = temp.Humidity;
                    }
                    await db.SaveChangesAsync();
                }
                message = new MqttApplicationMessageBuilder()
               .WithTopic($"{data.UniqueId}/terminal/AnalogQuantity")
               .WithPayload(data.ToJson())
               .WithAtMostOnceQoS()
               .WithRetainFlag()
               .Build();
            }
            else if (data is DeviceAlarmResultDto)//警报状态
            {
                var newValue = (data as DeviceAlarmResultDto).Data.ToStr();
                if (!newValue.Equals(deviceActor.AlarmStatus))
                {
                    using (var db = _serviceProvider.GetService<DataContext>())
                    {
                        db.AlarmStatuses.Add(new Model.AlarmStatus
                        {
                            EquipNum = data.UniqueId,
                            Value = newValue
                        });
                        await db.SaveChangesAsync();
                        deviceActor.AlarmStatus = newValue; 
                    }
                }
                message = new MqttApplicationMessageBuilder()
               .WithTopic($"{data.UniqueId}/terminal/DeviceAlarm")
               .WithPayload(data.ToJson())
               .WithAtMostOnceQoS()
               .WithRetainFlag()
               .Build();
            }
            else if (data is ChannelStatusResultDto)//开关量
            {
                var temp = data as ChannelStatusResultDto;
                var newValue = temp.Data.ToStr();
                if (!newValue.Equals(deviceActor.SwitchStatus))
                {
                    using (var db = _serviceProvider.GetService<DataContext>())
                    {
                        db.SwitchStatuses.Add(new Model.SwitchStatus
                        {
                            EquipNum = data.UniqueId,
                            Value = newValue
                        });
                        await db.SaveChangesAsync();
                        deviceActor.SwitchStatus = newValue;
                        deviceActor.SwStatus.AutomaticReclosing = temp.AutomaticReclosing;
                        deviceActor.SwStatus.Door = temp.Door;
                        deviceActor.SwStatus.Fan = temp.Fan;
                        deviceActor.SwStatus.Heating = temp.Heating;
                        deviceActor.SwStatus.LED = temp.LED;
                        deviceActor.SwStatus.NetworkLightningArrester = temp.NetworkLightningArrester;
                        deviceActor.SwStatus.Optical = temp.Optical;
                        deviceActor.SwStatus.PowerSupplyArrester = temp.PowerSupplyArrester;
                        deviceActor.SwStatus.Vedio = temp.Vedio;
                    }
                }
                message = new MqttApplicationMessageBuilder()
               .WithTopic($"{data.UniqueId}/terminal/ChannelStatus")
               .WithPayload(data.ToJson())
               .WithAtMostOnceQoS()
               .WithRetainFlag()
               .Build();
            }
            else if(data is CustomResultDto<SkynetTerminalCmdEnum>)
            {
                var val = data as CustomResultDto<SkynetTerminalCmdEnum>;
                //if(val.CustomEnum == CustomEnum.IPChange)
                //{
                //    message = new MqttApplicationMessageBuilder()
                //  .WithTopic($"{data.UniqueId}/terminal/{data.CmdName}")
                //  .WithPayload(data.ToJson())
                //  .WithExactlyOnceQoS()
                //  .Build();
                //}
                //else if(val.CustomEnum == CustomEnum.OnLine || val.CustomEnum == CustomEnum.OffLine)
                //{
                //    message = new MqttApplicationMessageBuilder()
                // .WithTopic($"{data.UniqueId}/terminal/status")
                // .WithPayload(data.ToJson())
                // .WithExactlyOnceQoS()
                // .WithRetainFlag()
                // .Build();
                //}
                
            }
            else
            {
                message = new MqttApplicationMessageBuilder()
                  .WithTopic($"{data.UniqueId}/terminal/Config/{data.CmdName}")
                  .WithPayload(data.ToJson())
                  .WithAtLeastOnceQoS()
                  .Build();
            }
            try
            {
                if (_mqttClient.IsConnected)
                {
                    await _mqttClient.PublishAsync(message);
                }
            }
            catch { }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
       
    }
}
