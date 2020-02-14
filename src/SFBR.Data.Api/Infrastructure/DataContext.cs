using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SFBR.Data.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFBR.Data.Api.Infrastructure
{
    public class DataContext : DbContext
    {
        /// <summary>
        /// 实时电压
        /// </summary>
        public DbSet<Voltage> Voltages { get; set; }
        /// <summary>
        /// 实时电流
        /// </summary>
        public DbSet<Current> Currents { get; set; }
        /// <summary>
        /// 实时温度
        /// </summary>
        public DbSet<Temperature> Temperatures { get; set; }
        /// <summary>
        /// 实时湿度
        /// </summary>
        public DbSet<Humidity>  Humidities { get; set; }
        /// <summary>
        /// 实时警报状态
        /// </summary>
        public DbSet<AlarmStatus> AlarmStatuses { get; set; }//只有变化时才保存
        /// <summary>
        /// 实时开关状态
        /// </summary>
        public DbSet<SwitchStatus> SwitchStatuses { get; set; }//只有变化时才保存

     
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Current>().Property(t=>t.Value).HasColumnType("decimal(18,1)");
            //modelBuilder.Entity<Voltage>().Property(t=>t.Value).HasColumnType("decimal(18,1)");
            //modelBuilder.Entity<Temperature>().Property(t=>t.Value).HasColumnType("decimal(18,1)");
            //modelBuilder.Entity<Humidity>().Property(t=>t.Value).HasColumnType("decimal(18,1)");
        }
    }

    public class DataContextDesignFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Server=.;Initial Catalog=SFBR.Log.Api;Integrated Security=true");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
