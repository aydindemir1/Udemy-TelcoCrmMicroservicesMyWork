using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Queries.GetList
{
    public class GetListProductOfferingResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public ProductOfferingStatus Status { get; set; }
        public string CategoryName { get; set; }
        public string ProductSpecificationName { get; set; }
        public List<ProductOfferingPriceResponse> ProductOfferingPrices { get; set; }
    }
}
