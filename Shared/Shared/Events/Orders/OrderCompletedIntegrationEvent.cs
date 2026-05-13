using Core.Abstractions.Events.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.Orders
{
    public record OrderCompletedIntegrationEvent(string OrderNumber, string BillingAccountId, string BillingAccountNumber, string BillingAccountName, string BillingAccountDescription, string BillingAccountType, string BillingAccountAddress, string CustomerId, string CustomerName, decimal TotalPrice, List<OrderItemDto> OrderItems) : IntegrationEvent
    {
    }

    public class OrderItemDto
    {
        public string ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }
        public int Quantity { get; set; }
    }
}
