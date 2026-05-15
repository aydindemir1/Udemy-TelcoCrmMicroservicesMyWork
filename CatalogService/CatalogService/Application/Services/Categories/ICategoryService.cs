using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Categories
{
    public interface ICategoryService
    {
        Task<Category> GetById(Guid id);
    }
}
