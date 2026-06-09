using Core.Abstractions.Cqrs.Query;
using Core.Application.Pipelines.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Queries.GetListPaginated
{
    public class GetListContactMediumQuery : IQuery<ContactMediumListModel>, IAuthenticationRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
