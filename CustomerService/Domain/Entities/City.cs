using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class City : BaseEntity<short>
    {
        public string Name { get; set; } //İstanbul,Ankara

        public virtual ICollection<District> Districts { get; set; }

        public City()
        {
            Districts = new HashSet<District>();
        }

        public City(short id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
