using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations
{
    public class UserOperationClaimConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasKey(uoc => uoc.Id);

            builder.Property(uoc => uoc.Id).IsRequired();
            builder.Property(uoc => uoc.UserId).IsRequired();
            builder.Property(uoc => uoc.OperationClaimId).IsRequired();
            builder.Property(uoc => uoc.CreatedDate).IsRequired();
            builder.Property(uoc => uoc.UpdatedDate);
            builder.Property(uoc => uoc.DeletedDate);

            builder.HasQueryFilter(uoc => !uoc.DeletedDate.HasValue);

            builder.HasOne(uoc => uoc.User);
            builder.HasOne(uoc => uoc.OperationClaim);
            builder.HasData(_seeds);

            builder.HasBaseType((string)null!);
        }


        private IEnumerable<UserOperationClaim> _seeds
        {
            get
            {
                yield return new()
                {
                    Id = Guid.Parse("c75d8ab3-79f3-47e6-9b0b-8b0904fedd7d"),
                    UserId = UserConfiguration.UserId,
                    OperationClaimId = OperationClaimConfiguration.AdminId
                };
            }
        }
    }
}
