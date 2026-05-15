using Application.Features.Brands.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, CreateBrandCommand>().ReverseMap();
            CreateMap<Brand, CreatedBrandResponse>().ReverseMap();
        }
    }
}
