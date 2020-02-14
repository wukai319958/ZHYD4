using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SFBR.Device.Domain.AggregatesModel.DeviceAggregate
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class TimedTask:SeedWork.Entity
    {
        protected TimedTask()
        {
            Id = Guid.NewGuid().ToString();
        }

        public TimedTask(string deviceId,int portNumber,bool enabled, string taskId,ExecType execType, ExecAction execAction, int executed,LoopType loopType,Moment moment,string loopMonent)
            : this()
        {
            DeviceId = deviceId;
            PortNumber = portNumber;
            Enabled = enabled;
            TaskId = taskId;
            ExecType = execType;
            ExecAction = execAction;
            Executed = executed;//未执行 执行中 已执行
            LoopType = loopType;
            Moment = moment;
            LoopMonent = loopMonent;
        }

        /// <summary>
        /// 
        /// </summary>
        [StringLength(50)]
        [Required]
        public string DeviceId { get;private set; }
        /// <summary>
        /// 回路编号
        /// </summary>
        public int PortNumber { get;private set; }
        /// <summary>
        /// 任务编号
        /// </summary>
        [StringLength(50)]
        public string TaskId { get;private set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 定时类型
        /// </summary>
        public ExecType ExecType { get;private set; }
        /// <summary>
        /// 执行的动作
        /// </summary>
        public ExecAction ExecAction { get; private set; }
        /// <summary>
        /// 是否已执行（只有只执行一次的才有该状态）
        /// </summary>
        public int Executed { get;private set; }
        /// <summary>
        /// 循环周期类型
        /// </summary>
        public LoopType LoopType { get;private set; }
        /// <summary>
        /// 执行的时刻
        /// </summary>
        public Moment Moment { get;private set; }
        /// <summary>
        /// 周期内参与循环的时间（比如，每周一执行或者每月的1号 10 20号执行,多个由英文逗号隔开）
        /// </summary>
        [StringLength(500)]
        public string LoopMonent { get;private set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get;private set; }
    }

    /// <summary>
    /// 执行类型
    /// </summary>
    public enum ExecType
    {
        /// <summary>
        /// 一次
        /// </summary>
        Onece = 0,
        /// <summary>
        /// 循环
        /// </summary>
        Loop = 1
    }
    /// <summary>
    /// 循环类型
    /// </summary>
    public enum LoopType
    {
        /// <summary>
        /// 分钟
        /// </summary>
        Minute = 0,
        /// <summary>
        /// 小时
        /// </summary>
        Hour = 1,
        /// <summary>
        /// 天
        /// </summary>
        Day =2,
        /// <summary>
        /// 周
        /// </summary>
        Week =3,
        /// <summary>
        /// 月
        /// </summary>
        Month =4,
        /// <summary>
        /// 年
        /// </summary>
        Year = 5
    }

    public enum ExecAction
    {
        /// <summary>
        /// 关
        /// </summary>
        Off = 0,
        /// <summary>
        /// 开
        /// </summary>
        On = 1
    }
}
