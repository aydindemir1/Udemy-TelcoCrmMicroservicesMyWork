using Core.Abstractions.Cqrs.Command;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Commands.Create
{
    public class CreateProductOfferingCommand : ICreateCommand<CreatedProductOfferingResponse>//, IAuthenticationRequest
    {
        public Guid CategoryId { get; set; }
        public Guid ProductSpecificationId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public ProductOfferingStatus Status { get; set; }
        public List<ProductOfferingPriceRequest> Prices { get; set; }
    }

    public class ProductOfferingPriceRequest
    {
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
        public PriceType PriceType { get; set; }
    }
}
