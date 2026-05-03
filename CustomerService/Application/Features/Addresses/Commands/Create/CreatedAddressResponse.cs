using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Commands.Create
{
    public class CreatedAddressResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public short DistrictId { get; set; }
        public AddressType Type { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Description { get; set; }
    }
}
