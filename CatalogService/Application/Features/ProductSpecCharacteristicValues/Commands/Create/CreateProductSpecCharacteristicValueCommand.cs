using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecCharacteristicValues.Commands.Create
{
    public class CreateProductSpecCharacteristicValueCommand : ICreateCommand<CreatedProductSpecCharacteristicValueResponse>, IAuthenticationRequest
    {
        public Guid ProductSpecCharacteristicId { get; set; }
        public string Value { get; set; } = null!;
        public bool IsDefault { get; set; }
    }
}
