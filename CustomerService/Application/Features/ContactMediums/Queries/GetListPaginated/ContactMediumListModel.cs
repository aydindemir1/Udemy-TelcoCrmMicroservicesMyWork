using Core.Abstractions.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ContactMediums.Queries.GetListPaginated
{
    public class ContactMediumListModel : BasePageableModel
    {
        public IList<GetListContactMediumResponse> Items { get; set; }
    }
}
