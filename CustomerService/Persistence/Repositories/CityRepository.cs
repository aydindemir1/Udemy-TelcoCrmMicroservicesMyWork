using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class CityRepository : EfRepositoryBase<City, short, CustomerDbContext>, ICityRepository
    {
        public CityRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
