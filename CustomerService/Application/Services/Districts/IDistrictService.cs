using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Districts
{
    public interface IDistrictService
    {
        Task<District> GetDistrictWithCityAsync(short id);
    }
}
