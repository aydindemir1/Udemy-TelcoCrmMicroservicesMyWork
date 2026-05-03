using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class District : BaseEntity<short>
    {
        public short CityId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }

        public virtual City City { get; set; }

        public District()
        {
            Addresses = new HashSet<Address>();
        }

        public District(short id, short cityId, string name)
        {
            Id = id;
            CityId = cityId;
            Name = name;
        }
    }
}
