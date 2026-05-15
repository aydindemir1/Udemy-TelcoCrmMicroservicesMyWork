using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Queries.GetList
{
    public class ProductSpecCharacteristicValueResponse
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
