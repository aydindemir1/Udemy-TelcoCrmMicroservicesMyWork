using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ContactDocument
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
    }
}
