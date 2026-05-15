using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Queries.GetList
{
    public class ProductSpecCharacteristicResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string ValueType { get; set; } = null!;
        public bool IsConfigurable { get; set; }
        public string? UnitOfMeasure { get; set; }
        public ICollection<ProductSpecCharacteristicValueResponse> ProductSpecCharacteristicValues { get; set; } = null!;
    }
}
