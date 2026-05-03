using Application.Repositories;
using Core.Abstractions.Cqrs.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Addresses.Queries.GetAddressByCustomerId
{
    public class GetAddressByCustomerIdQueryHandler : IQueryHandler<GetAddressByCustomerIdQuery, List<GetCustomerAddressResponse>>
    {
        private readonly IAddressRepository _addressRepository;

        public GetAddressByCustomerIdQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<List<GetCustomerAddressResponse>> Handle(GetAddressByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.GetListProjectedAsync(predicate: a => a.CustomerId == request.CustomerId,
                selector: a => new GetCustomerAddressResponse
                {
                    Id = a.Id,
                    Type = a.Type.ToString(),
                    FullAddress = $"{a.Street} No:{a.HouseNumber}, {a.District.Name}, {a.District.City.Name}"
                }
                );
            return addresses;
        }
    }
}
