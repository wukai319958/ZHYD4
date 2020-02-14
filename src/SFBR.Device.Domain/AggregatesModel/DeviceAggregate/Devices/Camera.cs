using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class Camera: Load
    {
       
        public Camera(string tentantId, string deviceName, string equipNum, bool enabled = false, string modelCode = null, string deviceIP = null, int devicePort = 0, string serverIP = null, int serverPort = 0, string description = null, string parentId = null, int connection = 0)
           : base(tentantId, deviceName, equipNum, nameof(Camera),2, enabled, modelCode, deviceIP, devicePort, serverIP, serverPort, description, parentId, connection)//实例化时默认端口为2

        {
            _deviceProps = new List<DeviceProp>()
            {
                new DeviceProp(this.Id,"account","账号",propValue:"admin",canRemove:false),
                new DeviceProp(this.Id,"password","密码",propValue:"123456",canRemove:false),
                new DeviceProp(this.Id,"serverPort","端口",propValue:"37777",canRemove:false),
                new DeviceProp(this.Id,"channelNum","通道",propValue:"1",canRemove:false),
            };
        }

        private List<DeviceProp> _deviceProps;

        /// <summary>
        /// 扩展属性。默认扩展：账号、密码、端口、通道号
        /// </summary>
        public IReadOnlyCollection<DeviceProp> DeviceProps => _deviceProps;

        //public override void SetConnetion(int connection)
        //{
        //    Connection = connection;//不需要触发离线上线事件，除非独立
        //}
    }
}
