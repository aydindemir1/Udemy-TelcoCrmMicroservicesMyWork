using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Queries.GetAddressByCustomerId
{
    public class GetCustomerAddressResponse
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string FullAddress { get; set; }
    }
}
