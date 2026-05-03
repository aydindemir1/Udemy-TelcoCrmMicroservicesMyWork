using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.DistrictId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.HouseNumber).IsRequired();
            builder.Property(x => x.Description).IsRequired();

            builder.HasOne(x => x.Customer);
            builder.HasOne(x => x.District);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
