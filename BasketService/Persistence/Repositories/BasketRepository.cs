using Application.Repositories;
using Core.Persistence.Repositories.Redis;
using Domain.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Repositories
{
    public class BasketRepository : RedisRepositoryBase<Basket>, IBasketRepository
    {
        public BasketRepository(IConnectionMultiplexer redis) : base(redis)
        {
        }
    }
}
