using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Commands
{
    public class CreatedProductSpecificationResponse
    {
        public Guid Id { get; set; }
        public short ModelId { get; set; }
        public string Name { get; set; } = null!;
        public string ProductType { get; set; }
        public string? Description { get; set; }
        public string LifecycleStatus { get; set; }
    }
}
