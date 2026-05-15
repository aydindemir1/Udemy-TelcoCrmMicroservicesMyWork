using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Commands.Create
{
    public class CreatedProductOfferingResponse
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid ProductSpecificationId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public ProductOfferingStatus Status { get; set; }
    }
}
