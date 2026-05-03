using Application.Repositories;
using AutoMapper;
using Core.Abstractions.Cqrs.Query;
using Core.Abstractions.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Queries.GetListPaginated
{
    public class GetListContactMediumQueryHandler : IQueryHandler<GetListContactMediumQuery, ContactMediumListModel>
    {
        private readonly IContactMediumRepository _contactMediumRepository;
        private readonly IMapper _mapper;

        public GetListContactMediumQueryHandler(IContactMediumRepository contactMediumRepository, IMapper mapper)
        {
            _contactMediumRepository = contactMediumRepository;
            _mapper = mapper;
        }

        public async Task<ContactMediumListModel> Handle(GetListContactMediumQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ContactMedium> contactMediums = await _contactMediumRepository.GetPaginatedAsync(predicate: null, pageIndex: request.PageIndex, pageSize: request.PageSize, asNoTracking: true, customize: query => query.Include(cm => cm.Customer), cancellationToken: cancellationToken);
            ContactMediumListModel responses = _mapper.Map<ContactMediumListModel>(contactMediums);
            return responses;
        }
    }
}
