using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductOfferingPrice : BaseEntity<Guid>
    {
        public Guid ProductOfferingId { get; set; }
        public string Name { get; set; } = null!;
        public PriceType PriceType { get; set; } // 0:Recurring, 1:OneTime
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";

        public virtual ProductOffering ProductOffering { get; set; } = null!;

        public ProductOfferingPrice()
        {

        }

        public ProductOfferingPrice(Guid id, Guid productOfferingId, string name, PriceType priceType, decimal amount, string currency)
        {
            Id = id;
            ProductOfferingId = productOfferingId;
            Name = name;
            PriceType = priceType;
            Amount = amount;
            Currency = currency;
        }
    }
}
