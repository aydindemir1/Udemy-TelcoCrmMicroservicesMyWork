using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ProductSpecCharacteristicValueRepository : EfRepositoryBase<ProductSpecCharacteristicValue, Guid, CatalogDbContext>, IProductSpecCharacteristicValueRepository
    {
        public ProductSpecCharacteristicValueRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
