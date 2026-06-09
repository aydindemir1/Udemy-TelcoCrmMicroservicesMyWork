using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Queries
{
    public class GetOrdersByCustomerIdQuery : IQuery<List<GetOrderResponse>>, IAuthenticationRequest
    {
        public string CustomerId { get; set; }
    }
}
