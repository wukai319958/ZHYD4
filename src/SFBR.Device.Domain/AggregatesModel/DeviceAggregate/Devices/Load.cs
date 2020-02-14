using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class Load : Device
    {
        public Load(string tentantId, string deviceName, string equipNum,string deviceTypeCode,int? portNumber = null, bool enabled = false, string modelCode = null, string deviceIP = null, int devicePort = 0, string serverIP = null, int serverPort = 0, string description = null, string parentId = null, int connection = 0)
          : base(tentantId, deviceName, equipNum, deviceTypeCode, enabled, modelCode, deviceIP, devicePort, serverIP, serverPort, description, parentId, connection, false)

        {
            PortNumber = portNumber;
        }

        /// <summary>
        /// 回路端口
        /// </summary>
        public int? PortNumber { get;private set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public string ChannelId { get; set; }
        //public Channel Channel { get; set; }

        public void SetPortNumber(int portNumber)
        {
            PortNumber = portNumber;
        }
    }
}
