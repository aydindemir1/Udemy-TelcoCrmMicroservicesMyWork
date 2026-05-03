using Application.Features.ContactMediums.Commands.Create;
using Application.Features.ContactMediums.Queries.GetListPaginated;
using AutoMapper;
using Core.Abstractions.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ContactMedium, CreateContactMediumCommand>().ReverseMap();
            CreateMap<ContactMedium, CreatedContactMediumResponse>().ReverseMap();
            CreateMap<ContactMedium, Queries.GetListPaginated.GetListContactMediumResponse>()
                .ForMember(dest => dest.CustomerNumber, opt => opt.MapFrom(src => src.Customer.CustomerNumber));
            CreateMap<IPaginate<ContactMedium>, ContactMediumListModel>().ReverseMap();
        }
    }
}
