using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Invoices.Queries
{
    public class GetInvoiceResponse
    {
        public Guid Id { get; set; }
        public string Number { get; private set; }
        public string OrderNumber { get; set; } = string.Empty;
        public string BillingAccountNumber { get; set; } = string.Empty;
        public string BillingAccountName { get; set; } = string.Empty;
        public string BillingAccountDescription { get; set; } = string.Empty;
        public string BillingAccountType { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<InvoiceItemDto> InvoiceItemDtos { get; set; }

        public GetInvoiceResponse(Guid id, string number, string orderNumber, string billingAccountNumber, string billingAccountName, string billingAccountDescription, string billingAccountType, string billingAddress, string customerName, decimal totalPrice, List<InvoiceItemDto> invoiceItemDtos)
        {
            Id = id;
            Number = number;
            OrderNumber = orderNumber;
            BillingAccountNumber = billingAccountNumber;
            BillingAccountName = billingAccountName;
            BillingAccountDescription = billingAccountDescription;
            BillingAccountType = billingAccountType;
            BillingAddress = billingAddress;
            CustomerName = customerName;
            TotalPrice = totalPrice;
            InvoiceItemDtos = invoiceItemDtos;
        }
    }

    public class InvoiceItemDto
    {
        public string ProductOfferingName { get; set; } = string.Empty;
        public string PriceName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
