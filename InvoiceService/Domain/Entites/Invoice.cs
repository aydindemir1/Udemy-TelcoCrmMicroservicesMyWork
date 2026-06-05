using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public class Invoice : BaseEntity<Guid>
    {
        public string Number { get; private set; }
        public string OrderNumber { get; set; } = string.Empty;
        public Guid BillingAccountId { get; set; }
        public string BillingAccountNumber { get; set; } = string.Empty;
        public string BillingAccountName { get; set; } = string.Empty;
        public string BillingAccountDescription { get; set; } = string.Empty;
        public string BillingAccountType { get; set; } = string.Empty;
        public string BillingAddress { get; set; } = string.Empty;

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

        public decimal TotalPrice { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }

        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public Invoice(string orderNumber, Guid billingAccountId, string billingAccountNumber, string billingAccountName, string billingAccountDescription, string billingAccountType, string billingAddress, Guid customerId, string customerName, decimal totalPrice, ICollection<InvoiceItem> invoiceItems)
        {
            OrderNumber = orderNumber;
            BillingAccountId = billingAccountId;
            BillingAccountNumber = billingAccountNumber;
            BillingAccountName = billingAccountName;
            BillingAccountDescription = billingAccountDescription;
            BillingAccountType = billingAccountType;
            BillingAddress = billingAddress;
            CustomerId = customerId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
            Number = GenerateNumber();
            InvoiceItems = invoiceItems;
        }

        private string GenerateNumber()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Random.Shared.Next(1000, 9999);
            return $"INV-{datePart}-{randomPart}";
        }
    }
}
