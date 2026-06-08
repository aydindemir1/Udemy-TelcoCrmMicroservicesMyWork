using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RefreshToken : Core.Security.Domain.Entities.RefreshToken
    {
        public virtual User User { get; set; }
    }
}
