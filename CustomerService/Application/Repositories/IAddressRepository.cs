using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Application.Repositories
{
    public interface IAddressRepository : IAsyncRepository<Address, Guid>, IRepository<Address, Guid>
    {
    }
}
