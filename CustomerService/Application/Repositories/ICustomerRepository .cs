using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface ICustomerRepository : IAsyncRepository<Customer, Guid>, IRepository<Customer, Guid>
    {
    }
}
