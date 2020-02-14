using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    public class Parameter:SeedWork.Entity
    {
        /// <summary>
        /// 地址（参数控制或者作用的对象）
        /// </summary>
        public int PortNumber { get; private set; }
        /// <summary>
        /// 参数作用的目标类型
        /// </summary>
        public TargetType TargetType { get; set; }
        /// <summary>
        /// 相
        /// </summary>
        public Phase Phase { get; private set; }
        /// <summary>
        /// 编号或者key。上限、下限
        /// </summary>
        public string ParameterName { get; set; }
        /// <summary>
        /// 配置项名称
        /// </summary>
        public string ParameterText { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 值类型
        /// </summary>
        public PropType ValueType { get; set; }//数据类型：值，字符串，复杂对象（指定类型后可以通过反射动态解析数据）
        /// <summary>
        /// 格式化规则
        /// </summary>
        public string Formatter { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }

    public enum TargetType
    {
        /// <summary>
        /// 主机的配置
        /// </summary>
        Master = 0,
        /// <summary>
        /// 传感器
        /// </summary>
        Sensor =1,
        /// <summary>
        /// 回路
        /// </summary>
        Channel = 2,
        /// <summary>
        /// 开关
        /// </summary>
        Switch = 3,
        /// <summary>
        /// 负载
        /// </summary>
        Load =4
    }
}
