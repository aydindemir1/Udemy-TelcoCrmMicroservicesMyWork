using Core.Abstractions.Events.Internal;
using Core.Persistence.Contexts;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Persistence.Contexts
{
    public class InvoiceDbContext : EfDbContextBase
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options, IDomainEventDispatcher? domainEventDispatcher = null) : base(options, domainEventDispatcher)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
