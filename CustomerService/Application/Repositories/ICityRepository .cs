using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface ICityRepository : IAsyncRepository<City, short>, IRepository<City, short>
    {
    }
}
