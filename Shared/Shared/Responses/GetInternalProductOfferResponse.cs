using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class GetInternalProductOfferResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PriceName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }
    }
}
