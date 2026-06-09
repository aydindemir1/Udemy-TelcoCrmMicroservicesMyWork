using Core.Abstractions.Events.Internal;
using Core.Persistence.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Persistence.Contexts
{
    public class CustomerDbContext : EfDbContextBase
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options , IDomainEventDispatcher? domainEventDispatcher = null
                                                                             ) : base(options, domainEventDispatcher)
        {


        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<ContactMedium> ContactMediums { get; set; }
        public DbSet<BillingAccount> BillingAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
