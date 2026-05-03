using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class BillingAccountRepository : EfRepositoryBase<BillingAccount, Guid, CustomerDbContext>, IBillingAccountRepository
    {
        public BillingAccountRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
