using Core.Abstractions.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application.Repositories
{
    public interface IModelRepository : IAsyncRepository<Model, short>
    {
    }
}
