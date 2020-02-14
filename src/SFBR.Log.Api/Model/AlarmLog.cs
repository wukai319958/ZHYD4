using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Model
{
    /// <summary>
    /// 警报日志
    /// </summary>
    public class AlarmLog
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        [StringLength(50)]
        public string DeviceId { get; set; }
        /// <summary>
        /// 设备唯一编号
        /// </summary>
        [StringLength(50)]
        public string EquipNum { get; set; }
        /// <summary>
        /// 警报编号
        /// </summary>
        [StringLength(50)]
        public string AlarmCode { get; set; }
        /// <summary>
        /// 报警名称
        /// </summary>
        [StringLength(450)]
        public string AlarmName { get; set; }
        /// <summary>
        /// 警报分组
        /// </summary>
        [StringLength(50)]
        public string GroupName { get; set; }
        /// <summary>
        /// 警报类型：持续性警报（温度过高）、瞬时警报（打火）
        /// </summary>
        public int AlarmType { get; set; }
        /// <summary>
        /// 警报来源：0 监控终端；1 回路；2 负载(一般只有离线、在线状态)；3 配件 ...
        /// 不同的设备警报的来源可能不一样，比如监控终端
        /// </summary>
        public int AlarmFrom { get; set; }
        /// <summary>
        /// 子设备编号
        /// </summary>
        [StringLength(50)]
        public string TargetCode { get; set; }
        /// <summary>
        /// 警报级别
        /// </summary>
        public int AlarmLevel { get; set; }
        /// <summary>
        /// 警报状态
        /// </summary>
        [StringLength(50)]
        public string AlarmStatus { get; set; }
        /// <summary>
        /// 发生警报时发送的消息
        /// </summary>
        [StringLength(450)]
        public string AlarmingDescription { get; set; }
        /// <summary>
        /// 解除警报时发送的模板
        /// </summary>
        [StringLength(450)]
        public string AlarmedDescription { get; set; }
        /// <summary>
        /// 规定修复时长
        /// </summary>
        public double RepairTime { get; set; }
        /// <summary>
        /// 报警时间
        /// </summary>
        public DateTime AlarmTime { get; set; }
        /// <summary>
        /// 是否解除（主机离线也算解除）
        /// </summary>
        public bool IsClear { get; set; }
        /// <summary>
        /// 解除报警时间
        /// </summary>
        public DateTime? ClearTime { get; set; }
        /// <summary>
        /// 解除原因：0 正常解除，1 主机掉线
        /// </summary>
        public int ClearReason { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 是否参与统计
        /// </summary>
        public bool IsStatistics { get; set; }
        /// <summary>
        /// 实时值（不是所有的警报都有）
        /// </summary>
        public double? RealData { get; set; }
        /// <summary>
        /// 报警上限
        /// </summary>
        public double? UpperLimit { get; set; }
        /// <summary>
        /// 报警下限
        /// </summary>
        public double? LowerLimit { get; set; }
        #region 处理相关：报修完成后或者需手动处理的才有数据，如果是设备自主解除没有数据，比如离线报警解除都是设备自动
        /// <summary>
        /// 是否处理
        /// </summary>
        public bool Disposed { get; set; }
        /// <summary>
        /// 处理结果
        /// </summary>
        [StringLength(400)]
        public string DisposalContent { get; set; }
        /// <summary>
        /// 处理人账号
        /// </summary>
        [StringLength(50)]
        public string DisposalUser { get; set; }
        /// <summary>
        /// 处理人姓名
        /// </summary>
        [StringLength(50)]
        public string DisposalUserName { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? DisposalTime { get; set; }
        #endregion
    }
}
