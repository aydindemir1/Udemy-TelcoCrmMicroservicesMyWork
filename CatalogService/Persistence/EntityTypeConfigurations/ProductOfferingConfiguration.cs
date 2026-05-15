using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class ProductOfferingConfiguration : IEntityTypeConfiguration<ProductOffering>
    {
        public void Configure(EntityTypeBuilder<ProductOffering> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.ProductSpecificationId).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(150);
            builder.Property(x => x.ValidFrom).IsRequired();
            builder.Property(x => x.ValidTo).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.DeletedDate);

            builder.HasMany(x => x.ProductOfferingPrices);
            builder.HasOne(x => x.Category);
            builder.HasOne(x => x.ProductSpecification);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
