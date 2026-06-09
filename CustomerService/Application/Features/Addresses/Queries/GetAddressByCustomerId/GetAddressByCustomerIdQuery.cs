using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Queries.GetAddressByCustomerId
{
    public class GetAddressByCustomerIdQuery : IQuery<List<GetCustomerAddressResponse>>, IAuthenticationRequest
    {
        public Guid CustomerId { get; set; }
    }
}
