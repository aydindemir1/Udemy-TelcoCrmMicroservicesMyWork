using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class IndividualCustomerConfiguration : IEntityTypeConfiguration<IndividualCustomer>
    {
        public void Configure(EntityTypeBuilder<IndividualCustomer> builder)
        {
            builder.ToTable("individual_customers");
            builder.Property(i => i.FirstName).IsRequired();
            builder.Property(i => i.LastName).IsRequired();
            builder.Property(i => i.NationalIdentity).IsRequired().HasMaxLength(11);
            builder.Property(i => i.BirthDate).IsRequired();
        }
    }
}
