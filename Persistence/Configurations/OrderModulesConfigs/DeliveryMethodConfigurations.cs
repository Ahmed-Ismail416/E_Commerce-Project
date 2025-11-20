using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations.OrderModulesConfigs
{
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");

            //Price
            builder
               .Property(p => p.Price)
               .HasColumnType("decimal(8,2)");
            //shortname
            builder
                .Property(p => p.ShortName)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
            //Description
            builder
                .Property(p => p.Description)
                .HasColumnType("varchar(200)")
                .HasMaxLength(100);
            // DeliveryTime
            builder
                .Property(p => p.DeliveryTime)
                .HasColumnType("varchar")
                .HasMaxLength(50);

        }

    }
}
