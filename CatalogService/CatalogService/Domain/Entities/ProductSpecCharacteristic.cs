using Core.Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductSpecCharacteristic : BaseEntity<Guid>
    {
        public Guid ProductSpecificationId { get; set; }

        public string Name { get; set; } = null!;
        public ProductSpecValueType ValueType { get; set; } // string, number, boolean
        public bool IsConfigurable { get; set; }
        public string? UnitOfMeasure { get; set; }

        public virtual ProductSpecification ProductSpecification { get; set; } = null!;

        public virtual ICollection<ProductSpecCharacteristicValue> ProductSpecCharacteristicValues { get; set; }

        public ProductSpecCharacteristic()
        {
            ProductSpecCharacteristicValues = new HashSet<ProductSpecCharacteristicValue>();
        }

        public ProductSpecCharacteristic(Guid id, Guid productSpecificationId, string name, ProductSpecValueType valueType, bool isConfigurable, string? unitOfMeasure)
        {
            Id = id;
            ProductSpecificationId = productSpecificationId;
            Name = name;
            ValueType = valueType;
            IsConfigurable = isConfigurable;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
