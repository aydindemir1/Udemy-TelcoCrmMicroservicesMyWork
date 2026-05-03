using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class CustomerRepository : EfRepositoryBase<Customer, Guid, CustomerDbContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
