using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CityId).IsRequired();

            builder.HasOne(x => x.City);
            builder.HasMany(x => x.Addresses);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
