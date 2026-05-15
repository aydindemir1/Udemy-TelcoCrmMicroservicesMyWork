using Application.Features.ProductSpecCharacteristicValues.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.ProductSpecCharacteristicValues.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductSpecCharacteristicValue, CreateProductSpecCharacteristicValueCommand>().ReverseMap();
            CreateMap<ProductSpecCharacteristicValue, CreatedProductSpecCharacteristicValueResponse>().ReverseMap();
        }
    }
}
