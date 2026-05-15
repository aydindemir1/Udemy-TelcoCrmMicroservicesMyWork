using Core.Domain;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Domain.Entities
{
    public class Brand : BaseEntity<short>
    {
        public string Name { get; set; }

        public virtual ICollection<Model> Models { get; set; }

        public Brand()
        {
            Models = new HashSet<Model>();

        }

        public Brand(short id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
