using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations
{
    public class EmailAuthenticatorConfiguration : IEntityTypeConfiguration<EmailAuthenticator>
    {
        public void Configure(EntityTypeBuilder<EmailAuthenticator> builder)
        {
            builder.HasKey(ea => ea.Id);

            builder.Property(ea => ea.Id).IsRequired();
            builder.Property(ea => ea.UserId).IsRequired();
            builder.Property(ea => ea.ActivationKey);
            builder.Property(ea => ea.IsVerified).IsRequired();
            builder.Property(ea => ea.CreatedDate).IsRequired();
            builder.Property(ea => ea.UpdatedDate);
            builder.Property(ea => ea.DeletedDate);

            builder.HasQueryFilter(ea => !ea.DeletedDate.HasValue);

            builder.HasOne(ea => ea.User);

            builder.HasBaseType((string)null!);
        }
    }
}
