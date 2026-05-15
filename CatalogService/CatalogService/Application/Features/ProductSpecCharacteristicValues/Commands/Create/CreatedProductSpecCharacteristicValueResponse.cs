using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristicValues.Commands.Create
{
    public class CreatedProductSpecCharacteristicValueResponse
    {
        public Guid Id { get; set; }
        public Guid ProductSpecCharacteristicId { get; set; }
        public string Value { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
