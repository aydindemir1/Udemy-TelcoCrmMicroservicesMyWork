using Application.Features.Districts.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Districts.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<District, CreateDistrictCommand>().ReverseMap();
            CreateMap<District, CreatedDistrictResponse>().ReverseMap();
        }
    }
}
