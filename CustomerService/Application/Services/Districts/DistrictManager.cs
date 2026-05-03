using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.Districts
{
    public class DistrictManager : IDistrictService
    {
        private readonly IDistrictRepository _districtRepository;

        public DistrictManager(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public async Task<District> GetDistrictWithCityAsync(short id)
        {
            return await _districtRepository.GetAsync(predicate: x => x.Id == id, customize: q => q.Include(x => x.City), asNoTracking: true);
        }
    }
}
