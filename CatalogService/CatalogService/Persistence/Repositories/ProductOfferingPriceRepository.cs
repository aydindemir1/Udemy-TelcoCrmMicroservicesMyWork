using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ProductOfferingPriceRepository : EfRepositoryBase<ProductOfferingPrice, Guid, CatalogDbContext>, IProductOfferingPriceRepository
    {
        public ProductOfferingPriceRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
