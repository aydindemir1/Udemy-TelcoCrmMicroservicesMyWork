using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ProductSpecCharacteristicValue : BaseEntity<Guid>
    {
        public Guid ProductSpecCharacteristicId { get; set; }
        public string Value { get; set; } = null!;
        public bool IsDefault { get; set; }

        public virtual ProductSpecCharacteristic ProductSpecCharacteristic { get; set; } = null!;

        public ProductSpecCharacteristicValue()
        {

        }

        public ProductSpecCharacteristicValue(Guid id, Guid productSpecCharacteristicId, string value, bool isDefault)
        {
            Id = id;
            ProductSpecCharacteristicId = productSpecCharacteristicId;
            Value = value;
            IsDefault = isDefault;
        }
    }
}
