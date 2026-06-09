using Core.Abstractions.Cqrs.Command;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Brands.Commands.Create
{
    public class CreateBrandCommand : ICreateCommand<CreatedBrandResponse>, IAuthenticationRequest
    {
        public string Name { get; set; }
    }
}
