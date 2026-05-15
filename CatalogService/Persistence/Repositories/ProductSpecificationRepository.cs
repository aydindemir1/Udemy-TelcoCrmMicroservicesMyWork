using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ProductSpecificationRepository : EfRepositoryBase<ProductSpecification, Guid, CatalogDbContext>, IProductSpecificationRepository
    {
        public ProductSpecificationRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
