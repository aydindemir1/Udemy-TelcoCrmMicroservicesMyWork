using Application.Repositories;
using AutoMapper;
using Core.Abstractions.Cqrs.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Queries.GetList
{
    public class GetListProductOfferingQueryHandler
     : IQueryHandler<GetListProductOfferingQuery, List<GetListProductOfferingResponse>>
    {
        private readonly IProductOfferingRepository _repository;
        private readonly IMapper _mapper;

        public GetListProductOfferingQueryHandler(
            IProductOfferingRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetListProductOfferingResponse>> Handle(GetListProductOfferingQuery request, CancellationToken cancellationToken)
        {
            var offerings = await _repository.GetListAsync(
                customize: query => query
                    .Include(po => po.Category)
                    .Include(po => po.ProductSpecification)
                    .Include(po => po.ProductOfferingPrices),
                asNoTracking: true,
                useSplitQuery: true,
                cancellationToken: cancellationToken
            );

            return _mapper.Map<List<GetListProductOfferingResponse>>(offerings);
        }
    }
}
