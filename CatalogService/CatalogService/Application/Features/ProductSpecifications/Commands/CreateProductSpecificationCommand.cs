using Core.Abstractions.Cqrs.Command;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Commands
{
    public class CreateProductSpecificationCommand : ICreateCommand<CreatedProductSpecificationResponse>//, IAuthenticationRequest
    {
        public short ModelId { get; set; }
        public string Name { get; set; } = null!;
        public ProductType ProductType { get; set; }
        public string? Description { get; set; }
        public LifecycleStatus LifecycleStatus { get; set; }
    }
}
