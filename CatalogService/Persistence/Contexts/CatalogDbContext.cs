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
    public class CatalogDbContext : EfDbContextBase
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options, IDomainEventDispatcher? domainEventDispatcher = null
                                                                          ) : base(options, domainEventDispatcher
                                                                                           )
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductOffering> ProductOfferings { get; set; }
        public DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public DbSet<ProductOfferingPrice> ProductOfferingPrices { get; set; }
        public DbSet<ProductSpecCharacteristic> ProductSpecCharacteristics { get; set; }
        public DbSet<ProductSpecCharacteristicValue> ProductSpecCharacteristicValues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
