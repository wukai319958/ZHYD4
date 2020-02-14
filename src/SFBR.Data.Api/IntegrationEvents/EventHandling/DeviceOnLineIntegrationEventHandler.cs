using MQTTnet;
using MQTTnet.Client;
using SFBR.Data.Api.IntegrationEvents.Events;
using SFBR.Device.Common.Tool;
using SFBR.EventBus.Abstractions;
using System;
using System.Threading.Tasks;

namespace SFBR.Log.Api.IntegrationEvents.EventHandling
{
    public class DeviceOnLineIntegrationEventHandler : IIntegrationEventHandler<DeviceOnLineIntegrationEvent>
    {

        private readonly IMqttClient _mqttClient;

        public DeviceOnLineIntegrationEventHandler(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient ?? throw new ArgumentNullException(nameof(mqttClient));
        }


        public async Task Handle(DeviceOnLineIntegrationEvent @event)
        {
            var message = new MqttApplicationMessageBuilder()
               .WithTopic($"{@event.EquipNum}/terminal/alarmmessage")
               .WithPayload(@event.ToJson())
               .WithExactlyOnceQoS()
               .WithMessageExpiryInterval(10)
               .Build();
            await _mqttClient.PublishAsync(message);
        }
    }
}
