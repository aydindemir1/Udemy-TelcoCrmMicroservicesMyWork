using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityTypeConfigurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OrderNumber).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.BillingAccountId).IsRequired();
            builder.Property(x => x.BillingAccountName).IsRequired();
            builder.Property(x => x.BillingAccountNumber).IsRequired();
            builder.Property(x => x.BillingAccountDescription).IsRequired();
            builder.Property(x => x.BillingAccountType).IsRequired();
            builder.Property(x => x.BillingAddress).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.CustomerName).IsRequired();
            builder.Property(x => x.TotalPrice).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate);
            builder.Property(x => x.DeletedDate);

            builder.HasMany(x => x.InvoiceItems);

            builder.HasQueryFilter(x => !x.DeletedDate.HasValue);
            builder.HasBaseType((string)null!);
        }
    }
}
