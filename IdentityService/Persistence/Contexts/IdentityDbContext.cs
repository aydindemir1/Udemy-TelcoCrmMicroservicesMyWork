using Core.Abstractions.Events.Internal;
using Core.Persistence.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Persistence.Contexts
{
    public class IdentityDbContext : EfDbContextBase
    {

        public IdentityDbContext(DbContextOptions<IdentityDbContext> options, IDomainEventDispatcher? domainEventDispatcher = null) : base(options, domainEventDispatcher)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<EmailAuthenticator> EmailAuthenticators { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
