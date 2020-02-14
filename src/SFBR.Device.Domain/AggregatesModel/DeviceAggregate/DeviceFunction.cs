using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 设备功能（主要解决设备参数的配置）
    /// </summary>
    public class DeviceFunction : SeedWork.Entity//, IPart,IParameter,IBrand
    {
        /********
         * 主题格式：设备类型/设备编号/设备功能
         * 1-5的端口（补光/视频/加热/光通/风扇）控制功能       隐藏  该功能独立
         * 1-5的端口（补光/视频/加热/光通/风扇）定时读取/配置
         * 1-5的端口（补光/视频/加热/光通/风扇）是否自动控制配置功能
         * 摄像头挂载位置读取/配置（视频/输出1/输出2）
         * 布撤防读取/配置
         * 报警阈值读取/配置（温度/湿度/电压/电流）
         * 摄像头（网络设备）IP读取/配置
         * 主机时间读取/配置
         * 摄像头故障检测时长读取/配置
         * 通道工作模式读取
         * 经纬度读取
         * 设备配置信息读取
         * *******/
        protected DeviceFunction()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DeviceFunction(string deviceId, string functionCode, string callbackCodes,string settingTypeName, int? portNumber = null, string setting = null, bool? enabled = null,bool lockSetting = false, string sort = null) 
            :this()
        {
            DeviceId = deviceId;
            PortNumber = portNumber;
            //FunctionName = functionName;
            //FunctionCode = functionCode;
            //FunctionType = functionType;
            Setting = setting;
            Enabled = enabled;
            LockSetting = lockSetting;
            //Description = description;
            FunctionCode = functionCode;
            Sort = sort;
            CallbackCodes = callbackCodes;
            SettingTypeName = settingTypeName;
        }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string FunctionCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get;private set; }
        /// <summary>
        /// 指令接收对象(-1表示主机)
        /// </summary>
        public int? PortNumber { get;private set; }
        /// <summary>
        /// 设置的值
        /// </summary>
        public string Setting { get;private set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? Enabled { get;private set; }
        /// <summary>
        /// 锁定配置，防止全局配置时覆盖值
        /// </summary>
        public bool LockSetting { get;private set; }
        /// <summary>
        /// 排序
        /// </summary>
        [StringLength(10)]
        public string Sort { get;private set; }
        ///// <summary>
        ///// 界面布局
        ///// </summary>
        //public string LayoutId { get;private set; }
        /// <summary>
        /// 一个功能会有多个命令码
        /// </summary>
        [StringLength(150)]
        public string CallbackCodes { get; set; }
        /// <summary>
        /// 值的类型
        /// </summary>
        [StringLength(2000)]//泛型会非常长
        public string SettingTypeName { get; set; }


        public void SetSetting(string setting)
        {
            Setting = setting;
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            Enabled = enabled;
        }

        public void SetLockSetting(bool setting)
        {
            LockSetting = setting;
        }
    }

}
