using Core.Domain;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Entites
{
    public class Order : BaseEntity<ObjectId>
    {
        public string Number { get; private set; }
        public string BillingAccountId { get; set; }
        public string BillingAccountNumber { get; set; }
        public string BillingAccountName { get; set; }
        public string BillingAccountDescription { get; set; }
        public string BillingAccountType { get; set; }
        public string BillingAddress { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
        }

        public Order(string billingAccountId, string billingAccountNumber, string billingAccountName, string billingAccountDescription, string billingAccountType, string billingAddress, string customerId, string customerName, DateTime createdDate, decimal totalPrice, List<OrderItem> items) : this()
        {
            Number = GenerateNumber();
            BillingAccountId = billingAccountId;
            BillingAccountNumber = billingAccountNumber;
            BillingAccountName = billingAccountName;
            BillingAccountDescription = billingAccountDescription;
            BillingAccountType = billingAccountType;
            BillingAddress = billingAddress;
            CustomerId = customerId;
            CustomerName = customerName;
            TotalPrice = totalPrice;
            Items = items;
            CreatedDate = createdDate;
        }

        private string GenerateNumber()
        {
            var datePart = DateTime.UtcNow.ToString("yyyyMMdd");
            var randomPart = Random.Shared.Next(1000, 9999);
            return $"ORD-{datePart}-{randomPart}";
        }
    }
}
