using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SFBR.Log.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Log.Api.Infrastructure
{
    public class LogContext : DbContext
    {
        /// <summary>
        /// 设备基础信息
        /// </summary>
        public DbSet<Device> Devices { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<AlarmLog> AlarmLogs { get; set; }
        /// <summary>
        /// 操作日志    
        /// </summary>
        public DbSet<ActionLog> ActionLogs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

    }

    public class LogContextDesignFactory : IDesignTimeDbContextFactory<LogContext>
    {
        public LogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LogContext>()
                .UseSqlServer("Server=.;Initial Catalog=SFBR.Log.Api;Integrated Security=true");

            return new LogContext(optionsBuilder.Options);
        }
    }
}
