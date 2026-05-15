using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class ProductSpecCharacteristicValueConfiguration : IEntityTypeConfiguration<ProductSpecCharacteristicValue>
    {
        public void Configure(EntityTypeBuilder<ProductSpecCharacteristicValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ProductSpecCharacteristicId).IsRequired();
            builder.Property(x => x.IsDefault).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.DeletedDate);

            builder.HasOne(x => x.ProductSpecCharacteristic);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
