using Application.Features.ProductSpecifications.Commands;
using Application.Features.ProductSpecifications.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.ProductSpecifications.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductSpecification, CreateProductSpecificationCommand>().ReverseMap();
            CreateMap<ProductSpecification, CreatedProductSpecificationResponse>().ReverseMap();
            CreateMap<ProductSpecification, GetListProductSpecificationResponse>().ReverseMap();
            CreateMap<ProductSpecCharacteristic, ProductSpecCharacteristicResponse>().ReverseMap();
            CreateMap<ProductSpecCharacteristicValue, ProductSpecCharacteristicValueResponse>().ReverseMap();
        }
    }
}
