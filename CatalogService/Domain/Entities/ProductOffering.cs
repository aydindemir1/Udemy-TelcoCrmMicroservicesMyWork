using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    // Ürün teklifi (Offering)
    public class ProductOffering : BaseEntity<Guid>
    {
        public Guid CategoryId { get; set; }
        public Guid ProductSpecificationId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public ProductOfferingStatus Status { get; set; } // 0:Active, 1:Suspended, 2:Retired

        public virtual Category Category { get; set; } = null!;
        public virtual ProductSpecification ProductSpecification { get; set; } = null!;

        public virtual ICollection<ProductOfferingPrice> ProductOfferingPrices { get; set; }

        public ProductOffering()
        {
            ProductOfferingPrices = new HashSet<ProductOfferingPrice>();
        }

        public ProductOffering(Guid id, Guid categoryId, Guid productSpecificationId, string name, string? description, DateTime validFrom, DateTime? validTo, ProductOfferingStatus status)
        {
            Id = id;
            CategoryId = categoryId;
            ProductSpecificationId = productSpecificationId;
            Name = name;
            Description = description;
            ValidFrom = validFrom;
            ValidTo = validTo;
            Status = status;
        }

        public void AddPrice(string name, decimal amount, string currency, PriceType priceType)
        {
            if (amount < 0)
                throw new ArgumentException("Fiyat negatif olamaz.");

            var price = new ProductOfferingPrice
            {
                Id = Guid.NewGuid(),
                ProductOfferingId = Id,
                Name = name,
                Amount = amount,
                Currency = currency,
                PriceType = priceType
            };
            ProductOfferingPrices.Add(price);
        }

        public static ProductOffering Create(Guid categoryId, Guid productSpecificationId, string name, string? description, DateTime validFrom, DateTime? validTo, ProductOfferingStatus status)
        {
            var productOffering = new ProductOffering
            { Id = Guid.NewGuid(), CategoryId = categoryId, ProductSpecificationId = productSpecificationId, Name = name, Description = description, ValidFrom = validFrom, ValidTo = validTo, Status = status };

            //if (validTo.HasValue)
            //    productOffering.AddDomainEvent(new ProductOfferingExpiryScheduledDomainEvent(productOffering.Id, validTo.Value));

            return productOffering;
        }


    }
}
