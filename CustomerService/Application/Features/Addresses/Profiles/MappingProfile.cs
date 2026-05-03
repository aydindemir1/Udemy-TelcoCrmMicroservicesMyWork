using Application.Features.Addresses.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, CreateAddressCommand>().ReverseMap();
            CreateMap<Address, CreatedAddressResponse>().ReverseMap();
        }
    }
}
