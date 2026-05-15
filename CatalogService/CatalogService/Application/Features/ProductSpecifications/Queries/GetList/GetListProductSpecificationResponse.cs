using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Queries.GetList
{
    public class GetListProductSpecificationResponse
    {
        public Guid Id { get; set; }
        public short ModelId { get; set; }
        public string ModelName { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string ProductType { get; set; } = null!;
        public string LifecycleStatus { get; set; } = null!;
        public ICollection<ProductSpecCharacteristicResponse> Characteristics { get; set; } = null!;
    }
}
