using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Commands.Device
{
    /// <summary>
    /// 创建设备
    /// </summary>
    public class CreateDeviceCommand : IRequest<Application.Queries.DeviceModel>
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceTypeCode { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipNum { get; set; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string DeviceIP { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string RegionId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string TentantId { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string ModelCode { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int DevicePort { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int ServerPort { get; set; }
        /// <summary>
        /// 启用（手动输入的设备默认未启用）
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 连接状态: 0 未连接； 1 连接
        /// </summary>
        public int Connection { get; set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Description { get; set; }

    }
}
