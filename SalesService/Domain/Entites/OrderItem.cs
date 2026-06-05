using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public class OrderItem
    {
        public string ProductOfferId { get; set; }
        public string ProductOfferName { get; set; }
        public string PriceName { get; set; }
        public string PriceType { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
