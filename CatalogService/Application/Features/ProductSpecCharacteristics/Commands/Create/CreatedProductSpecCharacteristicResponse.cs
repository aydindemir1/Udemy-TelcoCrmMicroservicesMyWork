using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristics.Commands.Create
{
    public class CreatedProductSpecCharacteristicResponse
    {
        public Guid Id { get; set; }
        public Guid ProductSpecificationId { get; set; }
        public string Name { get; set; } = null!;
        public string ValueType { get; set; } = null!;
        public bool IsConfigurable { get; set; }
        public string? UnitOfMeasure { get; set; }
    }
}
