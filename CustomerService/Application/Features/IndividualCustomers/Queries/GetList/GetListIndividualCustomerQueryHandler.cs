using Application.Repositories;
using AutoMapper;
using Core.Abstractions.Cqrs.Query;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.IndividualCustomers.Queries.GetList
{
    public class GetListIndividualCustomerQueryHandler : IQueryHandler<GetListIndividualCustomerQuery, List<GetListIndividualCustomerResponse>>
    {
        private readonly IIndividualCustomerRepository _individualCustomerRepository;
        private readonly IMapper _mapper;

        public GetListIndividualCustomerQueryHandler(IIndividualCustomerRepository individualCustomerRepository, IMapper mapper)
        {
            _individualCustomerRepository = individualCustomerRepository;
            _mapper = mapper;
        }

        public async Task<List<GetListIndividualCustomerResponse>> Handle(GetListIndividualCustomerQuery request, CancellationToken cancellationToken)
        {
            List<IndividualCustomer> individualCustomers = await _individualCustomerRepository.GetListAsync(useSplitQuery: true, customize: query => query.Include(c => c.Addresses).ThenInclude(a => a.District).ThenInclude(a => a.City), asNoTracking: true);
            List<GetListIndividualCustomerResponse> responses = _mapper.Map<List<GetListIndividualCustomerResponse>>(individualCustomers);
            return responses;
        }
    }
}
