using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class EmailAuthenticator : Core.Security.Domain.Entities.EmailAuthenticator
    {
        public virtual User User { get; set; }
    }
}
