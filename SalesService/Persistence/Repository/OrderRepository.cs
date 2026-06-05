using Application.Repositories;
using Core.Persistence.Repositories.MongoDb;
using Core.Persistence.Repositories.MongoDb.Configuration;
using Domain.Entites;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Persistence.Repository
{
    public class OrderRepository : MongoRepositoryBase<Order, ObjectId>, IOrderRepository
    {
        public OrderRepository(MongoConnectionSettings settings) : base(settings, "orders")
        {
        }
    }
}
