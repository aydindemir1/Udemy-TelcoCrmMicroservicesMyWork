using Core.Abstractions.Repositories.Redis;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Repositories
{
    public interface IBasketRepository : IRedisRepository<Basket>
    {
    }
}
