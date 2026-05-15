using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class ModelRepository : EfRepositoryBase<Model, short, CatalogDbContext>, IModelRepository
    {
        public ModelRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}
