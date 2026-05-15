using Application.Features.ProductOfferingPrices.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferingPrices.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductOfferingPrice, CreateProductOfferingPriceCommand>().ReverseMap();
            CreateMap<ProductOfferingPrice, CreatedProductOfferingPriceResponse>().ReverseMap();
        }
    }
}
