using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Queries
{
    public class GetOrdersByCustomerIdQuery : IQuery<List<GetOrderResponse>>//, IAuthenticationRequest
    {
        public string CustomerId { get; set; }
    }
}
