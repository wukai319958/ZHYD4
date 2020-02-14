//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using SFBR.Data.Api.Infrastructure;
//using SFBR.Data.Api.ViewModel;
//using SFBR.Device.Common;
//using SFBR.Device.Common.Commands;
//using SFBR.Device.Common.ConfigModel.SkynetTerminal.Models;
//using SFBR.Device.Common.ConfigModel.SkynetTerminal.Results;
//using SFBR.Device.Common.Tool;
//using SFBR_Client.SkynetTerminal;
//using System;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SFBR.Terminal.Web
//{
//    public class TerminalConsumer
//    {
//        internal static readonly ConcurrentDictionary<string, DeviceActor> valuePairs = new ConcurrentDictionary<string, DeviceActor>();
//        private readonly SkynetTerminalClient _client;
//        private IConfiguration _configuration;
//        private IServiceProvider _serviceProvider;

//        public static ConcurrentDictionary<string, DeviceActor> RealDataCaches => valuePairs;

//        public TerminalConsumer(IConfiguration configuration, IServiceProvider serviceProvider)
//        {
//            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
//            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
//            _client = new SkynetTerminalClient(configuration["EventBusConnection"], configuration["EventBusConnection"]);
//            _client.MessageCallBack += Client_MessageCallBack;
//            _client.Subscribe("#.SkynetTerminal.#");//订阅所有消息
//        }

//        private async void Client_MessageCallBack(BaseResultDto<SkynetTerminalCmdEnum> data)
//        {
//            await Save(data);
//        }

//        private Task Save(BaseResultDto<SkynetTerminalCmdEnum> data)
//        {
//            if (data == null) return Task.CompletedTask;
//            if (!valuePairs.TryGetValue(data.UniqueId, out DeviceActor deviceActor))
//            {
//                deviceActor = new DeviceActor();
//                valuePairs.TryAdd(data.UniqueId, deviceActor);
//            };

//            deviceActor.LastUpdate = DateTime.UtcNow;
//            if (data is AnalogQuantityResultDto)//模拟量
//            {
//                var temp = data as AnalogQuantityResultDto;
//                if (!temp.Voltage.Equals(deviceActor.Voltage))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                      db.Voltages.Add(new Model.Voltage
//                      {
//                          EquipNum = temp.UniqueId,
//                          Value = temp.Voltage,
//                          Position = "-1"
//                      });
//                    db.SaveChanges();
//                    deviceActor.Voltage = temp.Voltage; 
//                }
//                if (temp.Current.Equals(deviceActor.Current))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                    db.Currents.Add(new Model.Current
//                    {
//                        EquipNum = temp.UniqueId,
//                        Value = temp.Voltage,
//                        Position = "-1"
//                    });
//                    db.SaveChanges();
//                    deviceActor.Current = temp.Current; 
//                }
//                if (temp.Temperature.Equals(deviceActor.Temperature))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                    db.Temperatures.Add(new Model.Temperature
//                    {
//                        EquipNum = temp.UniqueId,
//                        Value = temp.Voltage,
//                        Position = "-1"
//                    });
//                    db.SaveChanges();
//                    deviceActor.Temperature = temp.Temperature; 
//                }
//                if (temp.Humidity.Equals(deviceActor.Humidity))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                    db.Humidities.Add(new Model.Humidity
//                    {
//                        EquipNum = temp.UniqueId,
//                        Value = temp.Voltage,
//                        Position = "-1"
//                    });
//                    db.SaveChanges();
//                    deviceActor.Humidity = temp.Humidity; 
//                }
//            }
//            else if (data is DeviceAlarmResultDto)//警报状态
//            {
//                var newValue = (data as DeviceAlarmResultDto).Data.ToStr();
//                if (!newValue.Equals(deviceActor.AlarmStatus))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                    db.AlarmStatuses.Add(new Model.AlarmStatus
//                    {
//                        EquipNum = data.UniqueId,
//                        Value = newValue
//                    });
//                    db.SaveChanges();
//                    deviceActor.AlarmStatus = newValue;
//                }
//            }
//            else if (data is ChannelStatusResultDto)//开关量
//            {
//                var newValue = (data as ChannelStatusResultDto).Data.ToStr();
//                if (newValue.Equals(deviceActor.SwitchStatus))
//                {
//                    var db = _serviceProvider.GetService<DataContext>();
//                    db.SwitchStatuses.Add(new Model.SwitchStatus
//                    {
//                        EquipNum = data.UniqueId,
//                        Value = newValue
//                    });
//                    db.SaveChanges();
//                    deviceActor.SwitchStatus = newValue;
//                }
//            }
//            return Task.CompletedTask;
//        }

       
//    }
//}
