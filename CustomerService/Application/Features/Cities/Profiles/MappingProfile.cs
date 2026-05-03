using Application.Features.Cities.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Cities.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CreateCityCommand>().ReverseMap();
            CreateMap<City, CreatedCityResponse>().ReverseMap();
        }
    }
}
