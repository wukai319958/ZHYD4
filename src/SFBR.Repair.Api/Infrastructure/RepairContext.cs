using Microsoft.EntityFrameworkCore;
using SFBR.Repair.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Repair.Api.Infrastructure
{
    public class RepairContext : DbContext
    {
        /// <summary>
        /// 设备基础信息
        /// </summary>
        public DbSet<Device> Devices { get; set; }
        /// <summary>
        /// 运维单位
        /// </summary>
        public DbSet<Company> Companies { get; set; }
        /// <summary>
        /// 任务单
        /// </summary>
        public DbSet<JobOrder> JobOrders { get; set; }
        /// <summary>
        /// 审核内容（聊天记录）
        /// </summary>
        public DbSet<AuditMessage> AuditMessages { get; set; }

        public RepairContext(DbContextOptions<RepairContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobOrder>().HasDiscriminator(t => t.OrderType).HasValue<JobOrder>(OrderType.Default).HasValue<RepairOrder>(OrderType.Repair);
        }
    }
}
