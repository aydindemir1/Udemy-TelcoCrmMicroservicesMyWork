using Core.Security.Domain.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).IsRequired();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(70);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired().HasMaxLength(11);
            builder.Property(u => u.AccessFailedCount);
            builder.Property(u => u.IsLockedOut);
            builder.Property(u => u.LockoutEnd);
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.AuthenticatorType).IsRequired();
            builder.Property(u => u.CreatedDate).IsRequired();
            builder.Property(u => u.UpdatedDate);
            builder.Property(u => u.DeletedDate);

            builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

            builder.HasMany(u => u.UserOperationClaims);
            builder.HasMany(u => u.RefreshTokens);
            builder.HasMany(u => u.EmailAuthenticators);
            builder.HasData(CreateAdminSeedUser());

            builder.HasBaseType((string)null!);
        }

        public static Guid UserId => Guid.Parse("02e915e9-82ec-43c7-a2a3-db0a2565f4db");
        //var hasher = new BcryptPasswordHasher();
        //var hash = hasher.HashPassword("123456789.Aa");
        //Console.WriteLine(hash);


        private User CreateAdminSeedUser()
        {

            var user = new User()
            {
                Id = UserId,
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@crm.com",
                AuthenticatorType = AuthenticatorType.None,
                Status = true,
                PasswordHash = "$2a$11$zhTvs/6sf.QBQDRlQsJQue27hirlbBOUN3g/Gb1aXB8bzMr7TVNBy"
            };
            return user;

        }
    }
}
