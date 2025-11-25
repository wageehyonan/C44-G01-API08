using Ecommerce_G02.Domain.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Configurations.OderEntitysConfig
{
    public class DeliveryMethodConfiguartion : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            //builder.ToTable("DeliveryMethods");
            builder.Property(p => p.Price).HasColumnType("decimal(8,2)");
            builder.Property(p => p.ShortName).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(p => p.Description).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(p => p.DeliveryTime).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
