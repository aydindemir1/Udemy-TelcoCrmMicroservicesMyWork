using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    // Temel ürün şablonu (Specification)
    public class ProductSpecification : BaseEntity<Guid>
    {
        public short ModelId { get; set; }
        public string Name { get; set; } = null!;
        public ProductType ProductType { get; set; } // 0:Service, 1:Device, 2:Bundle
        public string? Description { get; set; }
        public LifecycleStatus LifecycleStatus { get; set; } // 0:Planned, 1:Active, 2:Retired

        public virtual Model Model { get; set; }

        public virtual ICollection<ProductSpecCharacteristic> Characteristics { get; set; }

        public ProductSpecification()
        {
            Characteristics = new HashSet<ProductSpecCharacteristic>();
        }

        public ProductSpecification(Guid id, short modelId, string name, ProductType productType, string? description, LifecycleStatus lifecycleStatus)
        {
            Id = id;
            ModelId = modelId;
            Name = name;
            ProductType = productType;
            Description = description;
            LifecycleStatus = lifecycleStatus;
        }
    }
}
