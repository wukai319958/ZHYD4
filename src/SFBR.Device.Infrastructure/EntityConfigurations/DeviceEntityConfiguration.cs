using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFBR.Device.Domain.AggregatesModel.DeviceAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace SFBR.Device.Infrastructure.EntityConfigurations
{
    class DeviceEntityConfiguration : IEntityTypeConfiguration<Domain.AggregatesModel.DeviceAggregate.Device>
    {
        public void Configure(EntityTypeBuilder<Domain.AggregatesModel.DeviceAggregate.Device> builder)
        {
            //必须写配置初始化，否则继承的类属性字段无法指定，只能用系统自带的
            builder.ToTable("Devices", DeviceContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);


            //builder.Property<string>("EquipNum").IsRequired(true);
           


            //var navigation = builder.Metadata.FindNavigation(nameof(Device.Domain.AggregatesModel.DeviceAggregate.Device.Channels));

            //// DDD Patterns comment:
            ////Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.HasOne(p=>p.Parent)
            //    .WithMany(p=>p.Children)
            //    .HasForeignKey("ParentId")
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
