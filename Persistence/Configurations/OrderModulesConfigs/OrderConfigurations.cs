using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.OrderModulesConfigs
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            
            builder
               .Property(p => p.SubTotal)
               .HasColumnType("decimal(8,2)");

            builder.HasOne(d => d.DeliveryMethod)
                .WithMany()
                .HasForeignKey(d => d.DeliveryMethodId);

            builder.HasMany(o => o.Items)
                .WithOne();

            builder.OwnsOne(a => a.Address);
        }
    }
}
