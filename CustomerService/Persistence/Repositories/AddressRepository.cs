using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class AddressRepository : EfRepositoryBase<Address, Guid, CustomerDbContext>, IAddressRepository
    {
        public AddressRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
