using Application.Features.ProductSpecCharacteristics.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.ProductSpecCharacteristics.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProductSpecCharacteristic, CreateProductSpecCharacteristicCommand>().ReverseMap();
            CreateMap<ProductSpecCharacteristic, CreatedProductSpecCharacteristicResponse>().ReverseMap();
        }
    }
}
