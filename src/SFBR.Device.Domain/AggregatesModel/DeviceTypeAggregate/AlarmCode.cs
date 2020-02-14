using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceTypeAggregate
{
    /// <summary>
    /// 警报字典（初始化时需要添加种子数据，不提供维护接口）
    /// </summary>
    public class AlarmCode:SeedWork.Entity
    {
        protected AlarmCode()
        {

        }
        protected AlarmCode(string id)
            :this()
        {
            Id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
        }

        public AlarmCode(string code, string name, string groupName, bool isStatistics, bool enabled)
            :this(code)
        {
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            GroupName = groupName ;
            IsStatistics = isStatistics;
            Enabled = enabled;
        }

        /// <summary>
        /// 警报代码（协议解析的位置）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 警报名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 温度、电压、电流等等
        /// </summary>
        public string GroupName { get; private set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
    }
}
