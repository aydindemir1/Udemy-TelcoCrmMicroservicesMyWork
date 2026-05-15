using Application.Features.Categories.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Categories.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
        }
    }
}
