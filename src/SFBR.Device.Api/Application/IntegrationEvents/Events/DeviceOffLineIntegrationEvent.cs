using SFBR.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.IntegrationEvents.Events
{
    public class DeviceOffLineIntegrationEvent : IntegrationEvent
    {
        public const string OffLine = "OffLine";

        public DeviceOffLineIntegrationEvent(string deviceId, string deviceName, string deviceTypeCode, string modelCode, string equipNum, string regionId, string regionCode, string regionName, 
            string tentantId, string tentantName, string parentId,double latitude,double longitude)
        {
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            DeviceName = deviceName ?? throw new ArgumentNullException(nameof(deviceName));
            DeviceTypeCode = deviceTypeCode ?? throw new ArgumentNullException(nameof(deviceTypeCode));
            ModelCode = modelCode ?? throw new ArgumentNullException(nameof(modelCode));
            EquipNum = equipNum ?? throw new ArgumentNullException(nameof(equipNum));
            RegionId = regionId;
            RegionCode = regionCode;
            RegionName = regionName;
            TentantId = tentantId;
            TentantName = tentantName;
            ParentId = parentId;
            Latitude = latitude;
            Longitude = longitude;
            Alarm = new Alarm(Guid.NewGuid().ToString(), OffLine, "站点离线", "站点离线", 0, (int)Domain.AggregatesModel.DeviceTypeAggregate.AlarmFrom.Master, equipNum, 0, "1", $"{regionName}编号为{equipNum}的站点离线，请及时处理！", 0, DateTime.UtcNow, true, null, null, null);
        }
        #region 站点信息
        /// <summary>
        /// 站点id
        /// </summary>
        public string DeviceId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 站点编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 站点分配的区域
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RegionCode { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        public string RegionName { get; set; }
        /// <summary>
        /// 租户id
        /// </summary>
        public string TentantId { get; set; }
        /// <summary>
        /// 租户姓名
        /// </summary>
        public string TentantName { get; set; }
        /// <summary>
        /// 上一级设备
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }
        #endregion
        /// <summary>
        /// 警报消息
        /// </summary>
        public Alarm Alarm { get; set; }
    }
}
