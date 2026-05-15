using Application.Features.ProductOfferings.Commands.Create;
using Application.Features.ProductOfferings.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.ProductOfferings.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductOffering, CreateProductOfferingCommand>().ReverseMap();
            CreateMap<ProductOffering, CreatedProductOfferingResponse>().ReverseMap();
            CreateMap<ProductOfferingPrice, ProductOfferingPriceResponse>().ReverseMap();
            CreateMap<ProductOffering, GetListProductOfferingResponse>().ReverseMap();
        }
    }
}
