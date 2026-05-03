using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Districts.Commands.Create
{
    public class CreatedDistrictResponse
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public short CityId { get; set; }
    }
}
