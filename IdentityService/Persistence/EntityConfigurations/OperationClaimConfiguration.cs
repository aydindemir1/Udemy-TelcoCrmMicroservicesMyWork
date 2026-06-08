using Core.Security.Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations
{
    public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
    {
        public void Configure(EntityTypeBuilder<OperationClaim> builder)
        {
            builder.HasKey(oc => oc.Id);

            builder.Property(oc => oc.Id).IsRequired();
            builder.Property(oc => oc.Name).IsRequired();
            builder.Property(oc => oc.CreatedDate).IsRequired();
            builder.Property(oc => oc.UpdatedDate);
            builder.Property(oc => oc.DeletedDate);

            builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

            builder.HasBaseType((string)null!);

            builder.HasData(_seeds);
        }

        public static Guid AdminId => Guid.Parse("a182e328-bb26-407b-9aea-4cc2048ef5b1");
        private IEnumerable<OperationClaim> _seeds
        {
            get
            {
                yield return new() { Id = AdminId, Name = GeneralOperationClaim.Admin };
            }
        }
    }
}
