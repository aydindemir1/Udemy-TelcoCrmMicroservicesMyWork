using Application.Repositories;
using Core.Persistence.Repositories.EfCore;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{

    public class ContactMediumRepository : EfRepositoryBase<ContactMedium, Guid, CustomerDbContext>, IContactMediumRepository
    {
        public ContactMediumRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
