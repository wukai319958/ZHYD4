using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Repair.Api.Model
{
    /// <summary>
    /// 任务单（展会暂不开发）
    /// </summary>
    public class JobOrder
    {
        public string Id { get; set; }
        /// <summary>
        /// 任务单号
        /// </summary>
        [StringLength(50)]
        public string OrderNo { get; set; }
        /// <summary>
        /// 派单人
        /// </summary>
        [StringLength(50)]
        public string Sender { get; set; }
        /// <summary>
        /// 派单类型：0 系统派单；1 手动派单；
        /// </summary>
        public int SendType { get; set; }
        /// <summary>
        /// 任务单状态
        /// </summary>
        [StringLength(50)]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// 任务单类型（普通任务单与站点无关、站点维护任务单）
        /// </summary>
        public OrderType OrderType { get; set; }
        /// <summary>
        /// 任务单来源（一般为系统或服务的编号）
        /// </summary>
        [StringLength(50)]
        public string WhereFrom { get; set; }
        #region 处理单位
        /// <summary>
        /// 处理单位
        /// </summary>
        [StringLength(50)]
        public string CompanyId { get; set; }
        /// <summary>
        /// 处理单位名称
        /// </summary>
        [StringLength(50)]
        public string CompanyName { get; set; }
        /// <summary>
        /// 处理单位负责人电话
        /// </summary>
        [StringLength(50)]
        public string CompanyPhone { get; set; }
        /// <summary>
        /// 处理人员
        /// </summary>
        [StringLength(50)]
        public string OprationId { get; set; }
        /// <summary>
        /// 处理人员姓名
        /// </summary>
        [StringLength(50)]
        public string OprationName { get; set; }
        /// <summary>
        /// 处理人员电话
        /// </summary>
        [StringLength(50)]
        public string OprationPhone { get; set; }
        #endregion
        /// <summary>
        /// 默认修复时长
        /// </summary>
        public double RepairTime { get; set; }
        /// <summary>
        /// 任务单描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 派单时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 审核信息
        /// </summary>
        public List<AuditMessage> Messages { get; set; }

    }

    /// <summary>
    /// 任务单状态
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// 开始
        /// </summary>
        Start = 0,
        /// <summary>
        /// 完成
        /// </summary>
        Finish = 1
    }

    public enum OrderType
    {
        /// <summary>
        /// 普通任务单
        /// </summary>
        Default = 0,
        /// <summary>
        /// 报修任务单
        /// </summary>
        Repair = 1

    }
}
