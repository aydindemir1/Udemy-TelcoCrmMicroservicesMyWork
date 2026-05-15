using Application.Repositories;
using AutoMapper;
using Core.Abstractions.Cqrs.Query;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductSpecifications.Queries.GetList
{
    public class GetProductSpecificationDetailQueryHandler : IQueryHandler<GetListProductSpecificationQuery, List<GetListProductSpecificationResponse>>
    {
        private readonly IProductSpecificationRepository _repository;
        private readonly IMapper _mapper;

        public GetProductSpecificationDetailQueryHandler(IProductSpecificationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetListProductSpecificationResponse>> Handle(GetListProductSpecificationQuery request, CancellationToken cancellationToken)
        {
            List<ProductSpecification> productSpecifications = await _repository.GetListAsync(predicate: x => x.Id == request.Id, useSplitQuery: true, asNoTracking: true, customize: query => query.Include(x => x.Model).Include(x => x.Characteristics).ThenInclude(c => c.ProductSpecCharacteristicValues));

            List<GetListProductSpecificationResponse> responses = _mapper.Map<List<GetListProductSpecificationResponse>>(productSpecifications);
            return responses;
        }
    }
}
