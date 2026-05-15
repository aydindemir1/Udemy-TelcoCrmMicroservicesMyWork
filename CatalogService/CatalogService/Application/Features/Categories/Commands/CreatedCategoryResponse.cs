using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Categories.Commands
{
    public class CreatedCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
