using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }
        public int Quantity { get; set; } = 1;

        public BasketItem()
        {

        }

        public BasketItem(Guid id, Guid productOfferId, string productOfferName, string priceName, decimal unitPrice, string priceType, int quantity)
        {
            Id = id;
            ProductOfferId = productOfferId;
            ProductOfferName = productOfferName;
            PriceName = priceName;
            UnitPrice = unitPrice;
            PriceType = priceType;
            Quantity = quantity;
        }

        public void UpdateQuantity(int additionalQuantity, decimal currentPrice)
        {
            if (additionalQuantity <= 0 && (Quantity + additionalQuantity) < 0)
                throw new ArgumentException("Geçersiz miktar güncellemesi");

            Quantity += additionalQuantity;
            UnitPrice = currentPrice;
        }


    }
}
