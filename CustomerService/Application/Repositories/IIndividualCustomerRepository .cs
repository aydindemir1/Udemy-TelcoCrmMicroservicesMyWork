using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IIndividualCustomerRepository : IAsyncRepository<IndividualCustomer, Guid>, IRepository<IndividualCustomer, Guid>
    {
    }
}
