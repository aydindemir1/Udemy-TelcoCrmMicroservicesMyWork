using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Responses
{
    public class GetInternalBasketResponse
    {
        public Guid BillingAccountId { get; set; }
        public List<GetInternalBasketItemResponse> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class GetInternalBasketItemResponse
    {
        public Guid ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public string PriceType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
