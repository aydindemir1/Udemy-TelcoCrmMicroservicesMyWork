using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class BrandRepository : EfRepositoryBase<Brand, short, CatalogDbContext>, IBrandRepository
    {
        public BrandRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
