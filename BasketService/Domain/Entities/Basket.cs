using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Basket : IRedisEntity
    {
        public Guid Id { get; set; }
        public Guid BillingAccountId { get; set; }
        public List<BasketItem> Items { get; set; }
        public decimal TotalPrice => Items.Sum(x => x.UnitPrice * x.Quantity);

        public Basket(Guid id, Guid billingAccountId, List<BasketItem> items) : this()
        {
            Id = id;
            BillingAccountId = billingAccountId;
            Items = items;
        }

        public Basket(Guid id, Guid billingAccountId) : this()
        {
            Id = id;
            BillingAccountId = billingAccountId;
        }

        public Basket()
        {
            Items = new List<BasketItem>();
        }

        public void AddOrUpdateItem(Guid offerId, string offerName, string offerPriceName, decimal offerUnitPrice, string offerPriceType, int quantity)
        {
            var existingItem = Items.FirstOrDefault(x => x.ProductOfferId == offerId);
            if (existingItem != null)
                existingItem.UpdateQuantity(quantity, offerUnitPrice);
            else
            {
                Items.Add(new BasketItem(Guid.NewGuid(), offerId, offerName, offerPriceName, offerUnitPrice, offerPriceType, quantity));
            }
        }

        public void RemoveItemById(Guid itemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == itemId);

            if (item == null)
                throw new ArgumentException("Item bulunamadı");

            Items.Remove(item);
        }

        public string GetRedisKey()
        {
            return $"basket:{BillingAccountId}";
        }
    }
}
