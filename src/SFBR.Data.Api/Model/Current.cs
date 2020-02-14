using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.Model
{
    /// <summary>
    /// 实时电流
    /// </summary>
    public class Current
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
