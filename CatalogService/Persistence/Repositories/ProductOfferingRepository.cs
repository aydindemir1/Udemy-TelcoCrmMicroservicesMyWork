using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ProductOfferingRepository : EfRepositoryBase<ProductOffering, Guid, CatalogDbContext>, IProductOfferingRepository
    {
        public ProductOfferingRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
