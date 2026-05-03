using Core.Abstractions.Cqrs.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Districts.Commands.Create
{
    public class CreateDistrictCommand : ICreateCommand<CreatedDistrictResponse>
    {
        public string Name { get; set; }
        public short CityId { get; set; }
    }
}
