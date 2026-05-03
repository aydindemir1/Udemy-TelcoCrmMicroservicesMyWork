using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class DistrictRepository : EfRepositoryBase<District, short, CustomerDbContext>, IDistrictRepository
    {
        public DistrictRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
