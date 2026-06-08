using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id).IsRequired();
            builder.Property(rt => rt.UserId).IsRequired();
            builder.Property(rt => rt.Token).IsRequired();
            builder.Property(rt => rt.Expires).IsRequired();
            builder.Property(rt => rt.CreatedByIp).IsRequired();
            builder.Property(rt => rt.Revoked);
            builder.Property(rt => rt.RevokedByIp);
            builder.Property(rt => rt.ReplacedByToken);
            builder.Property(rt => rt.ReasonRevoked);
            builder.Property(rt => rt.CreatedDate).IsRequired();
            builder.Property(rt => rt.UpdatedDate);
            builder.Property(rt => rt.DeletedDate);

            builder.HasQueryFilter(rt => !rt.DeletedDate.HasValue);

            builder.HasOne(rt => rt.User);

            builder.HasBaseType((string)null!);
        }
    }
}
