using Application.Features.Models.Commands;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Model, CreateModelCommand>().ReverseMap();
            CreateMap<Model, CreatedModelResponse>().ReverseMap();
        }
    }
}
