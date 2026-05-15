using Core.Abstractions.Cqrs.Command;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristics.Commands.Create
{
    public class CreateProductSpecCharacteristicCommand : ICreateCommand<CreatedProductSpecCharacteristicResponse>//, IAuthenticationRequest
    {
        public Guid ProductSpecificationId { get; set; }
        public string Name { get; set; } = null!;
        public ProductSpecValueType ValueType { get; set; }
        public bool IsConfigurable { get; set; }
        public string? UnitOfMeasure { get; set; }
    }
}
