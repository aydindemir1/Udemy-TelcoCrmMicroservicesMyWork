using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Models.Commands
{
    public class CreateModelCommand : ICreateCommand<CreatedModelResponse>//, IAuthenticationRequest
    {
        public string Name { get; set; }
        public short BrandId { get; set; }
    }
}
