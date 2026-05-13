using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class GetInternalBillingAccountResponse
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressHouseNumber { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
    }
}
