using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferingPrices.Commands.Create
{
    public class CreateProductOfferingPriceCommand : ICreateCommand<CreatedProductOfferingPriceResponse>, IAuthenticationRequest
    {
        public Guid ProductOfferingId { get; set; }
        public string Name { get; set; } = null!;
        public PriceType PriceType { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = null!;
    }
}
