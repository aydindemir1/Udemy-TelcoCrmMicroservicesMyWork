using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IProductOfferingPriceRepository : IAsyncRepository<ProductOfferingPrice, Guid>
    {
    }
}
