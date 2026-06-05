using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Orders.Queries
{
    public class GetOrderResponse
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string BillingAccountId { get; set; }
        public string BillingAccountNumber { get; set; }
        public string BillingAccountName { get; set; }
        public string BillingAccountDescription { get; set; }
        public string BillingAccountType { get; set; }
        public string BillingAddress { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<GetOrderItemResponse> Items { get; set; }
    }

    public class GetOrderItemResponse
    {
        public string ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public string PriceType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
