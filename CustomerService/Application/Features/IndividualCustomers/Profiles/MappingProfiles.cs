using Application.Features.IndividualCustomers.Commands.Create;
using Application.Features.IndividualCustomers.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateIndividualCustomerCommand, IndividualCustomer>()
                .ConstructUsing(src => new IndividualCustomer(
                   src.FirstName,
                   src.LastName,
                   src.NationalIdentity
                ))
                .ReverseMap();
            CreateMap<IndividualCustomer, CreateIndividualCustomerCommand>().ReverseMap();
            CreateMap<IndividualCustomer, CreatedIndividualCustomerResponse>().ReverseMap();
            CreateMap<Address, CustomerAddressResponse>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => $"{src.Street} No: {src.HouseNumber} {src.District.Name}/{src.District.City.Name}"));
            CreateMap<IndividualCustomer, GetListIndividualCustomerResponse>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));

        }
    }
}
