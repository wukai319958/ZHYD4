using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.ViewModel
{
    public class DeviceActor
    {
        public DeviceActor()
        {
            SwStatus = new SwitchStatus();
        }
        public string EquipNum { get; set; }
        /// <summary>
        /// 实时警报状态
        /// </summary>
        public string AlarmStatus { get; set; }
        /// <summary>
        /// 实时开关状态
        /// </summary>
        public string SwitchStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SwitchStatus SwStatus { get; set; }
        /// <summary>
        /// 实时电流
        /// </summary>
        public double Current { get; set; }
        /// <summary>
        /// 实时电压
        /// </summary>
        public double Voltage { get; set; }
        /// <summary>
        /// 实时温度
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// 实时湿度
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// 最后一次刷更新时间
        /// </summary>
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// ISO标准时间字符串
        /// </summary>
        public string LastUpdateString => LastUpdate.ToString("yyyy-MM-ddTHH:mm:ss.zzzz", System.Globalization.DateTimeFormatInfo.CurrentInfo);
    }

    public class SwitchStatus
    {
        /// <summary>
        /// 视频通道状态，（1：打开   0关闭）
        /// </summary>
        public int Vedio { get; set; }
        /// <summary>
        /// 光通通道状态,（1：打开   0关闭）
        /// </summary>
        public int Optical { get; set; }
        /// <summary>
        /// 补光灯,（1：打开   0关闭）
        /// </summary>
        public int LED { get; set; }
        /// <summary>
        /// 加热,（1：打开   0关闭）
        /// </summary>
        public int Heating { get; set; }
        /// <summary>
        /// 风扇,（1：打开   0关闭）
        /// </summary>
        public int Fan { get; set; }
        /// <summary>
        /// 门磁，箱门状态,（1：打开   0关闭）
        /// </summary>
        public int Door { get; set; }
        /// <summary>
        /// 电源防雷器状态（1：断开   0正常）
        /// </summary>
        public int PowerSupplyArrester { get; set; }
        /// <summary>
        /// 网络防雷器状态（1：断开   0正常）
        /// </summary>
        public int NetworkLightningArrester { get; set; }
        /// <summary>
        /// 自动重合闸状态（1：合闸   0断开）
        /// </summary>
        public int AutomaticReclosing { get; set; }
    }
}
