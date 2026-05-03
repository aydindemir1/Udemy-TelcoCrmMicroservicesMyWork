using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class IndividualCustomerRepository : EfRepositoryBase<IndividualCustomer, Guid, CustomerDbContext>, IIndividualCustomerRepository
    {
        public IndividualCustomerRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
