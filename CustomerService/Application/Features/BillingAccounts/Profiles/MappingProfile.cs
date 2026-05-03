using Application.Features.BillingAccounts.Commands.Create;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.BillingAccounts.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BillingAccount, CreateBillingAccountCommand>().ReverseMap();
            CreateMap<BillingAccount, CreatedBillingAccountResponse>().ReverseMap();

        }
    }
}
