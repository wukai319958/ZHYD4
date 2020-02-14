using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Device.Api.Application.Queries
{
    /// <summary>
    /// 按状态站点统计结果
    /// </summary>
    public class TerminalCountResult
    {
        /// <summary>
        /// 在线（正常）
        /// </summary>
        public int Normal { get; set; }
        /// <summary>
        /// 在线（报警）
        /// </summary>
        public int Alarm { get; set; }
        /// <summary>
        /// 离线
        /// </summary>
        public int OffLine { get; set; }
    }
    /// <summary>
    /// 按状态摄像机统计结果
    /// </summary>
    public class CameraCountResult
    {
        /// <summary>
        /// 在线（正常）
        /// </summary>
        public int OnLine { get; set; }
        /// <summary>
        /// 离线
        /// </summary>
        public int OffLine { get; set; }
    }

    /// <summary>
    /// 站点排名
    /// </summary>
    public class TerminalRerecoveryResult
    {
        /// <summary>
        /// 站点id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 修复时长
        /// </summary>
        public double RepairTime { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public int Ranking { get; set; }

    }



}
