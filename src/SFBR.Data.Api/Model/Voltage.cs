using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Voltage//每一种设备一个实时数据库，每一个库再分电压电流温度
    {
        [StringLength(50)]
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 设备编号
        /// </summary>
        [StringLength(50)]
        public string EquipNum { get; set; }
        /// <summary>
        /// 监测的位置
        /// </summary>
        [StringLength(50)]
        public string Position { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; private set; } = DateTime.UtcNow;
    }
}
