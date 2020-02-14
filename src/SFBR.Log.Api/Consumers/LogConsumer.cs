using MQTTnet;
using SFBR.Log.Api.Infrastructure;
using SFBR.Log.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Consumers
{
    public class LogConsumer
    {
        private readonly Infrastructure.LogContext _logContext;

        public LogConsumer(LogContext logContext)
        {
            _logContext = logContext ?? throw new ArgumentNullException(nameof(logContext));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            var json = e.ApplicationMessage.Payload.ToStr();
#if DEBUG
            Console.WriteLine(json);
#endif
            var log = json.ToObj<ActionLog>();
            _logContext.ActionLogs.Add(log);
            await _logContext.SaveChangesAsync();
        }
    }
}
