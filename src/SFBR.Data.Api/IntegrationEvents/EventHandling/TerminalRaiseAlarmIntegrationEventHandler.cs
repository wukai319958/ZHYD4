using MQTTnet;
using MQTTnet.Client;
using SFBR.Data.Api.IntegrationEvents.Events;
using SFBR.Device.Common.Tool;
using SFBR.EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.IntegrationEvents.EventHandling
{
    public class TerminalRaiseAlarmIntegrationEventHandler :
        IIntegrationEventHandler<TerminalRaiseAlarmIntegrationEvent>
    {
        private readonly IMqttClient _mqttClient;

        public TerminalRaiseAlarmIntegrationEventHandler(IMqttClient mqttClient)
        {
            _mqttClient = mqttClient ?? throw new ArgumentNullException(nameof(mqttClient));
        }


        public async Task Handle(TerminalRaiseAlarmIntegrationEvent @event)
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
