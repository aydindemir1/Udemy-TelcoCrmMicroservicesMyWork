using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Models.Commands
{
    public class CreatedModelResponse
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public short BrandId { get; set; }
    }
}
