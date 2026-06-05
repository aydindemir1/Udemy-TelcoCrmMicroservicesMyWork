using Core.Abstractions.Repositories.MongoDb;
using Domain.Entites;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Repositories
{
    public interface IOrderRepository : IMongoAsyncRepository<Order, ObjectId>
    {
    }
}
