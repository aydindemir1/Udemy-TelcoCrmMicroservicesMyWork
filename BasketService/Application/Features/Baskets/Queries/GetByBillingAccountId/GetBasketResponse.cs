using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Baskets.Queries.GetByBillingAccountId
{
    public class GetBasketResponse
    {
        public Guid BillingAccountId { get; set; }
        public List<GetBasketItemResponse> Items { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class GetBasketItemResponse
    {
        public Guid ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public string PriceType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
