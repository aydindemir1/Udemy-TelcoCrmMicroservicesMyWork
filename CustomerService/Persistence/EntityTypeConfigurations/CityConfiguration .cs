using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();


            builder.HasMany(x => x.Districts);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
