using Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Model : BaseEntity<short>
    {
        public short BrandId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductSpecification> ProductSpecifications { get; set; }

        public virtual Brand Brand { get; set; }

        public Model()
        {
            ProductSpecifications = new HashSet<ProductSpecification>();
        }

        public Model(short id, short brandId, string name)
        {
            Id = id;
            BrandId = brandId;
            Name = name;
        }
    }
}
