using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entites
{
    public class InvoiceItem : BaseEntity<Guid>
    {

        public Guid InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; } // EF Core için required

        public Guid ProductOfferingId { get; set; }
        public string ProductOfferingName { get; set; } = string.Empty;
        public string PriceName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; } = string.Empty;
        public int Quantity { get; set; }

    }
}
