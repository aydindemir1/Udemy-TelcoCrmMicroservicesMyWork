using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Cities.Commands.Create
{
    public class CreateCityCommand : ICreateCommand<CreatedCityResponse>
    {
        public string Name { get; set; }
    }
}
