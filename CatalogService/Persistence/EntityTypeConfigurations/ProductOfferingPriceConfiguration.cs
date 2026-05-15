using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class ProductOfferingPriceConfiguration : IEntityTypeConfiguration<ProductOfferingPrice>
    {
        public void Configure(EntityTypeBuilder<ProductOfferingPrice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProductOfferingId).IsRequired();
            builder.Property(x => x.PriceType).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.DeletedDate);

            builder.HasOne(x => x.ProductOffering);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
