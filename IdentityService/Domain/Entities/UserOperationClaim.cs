using Core.Security.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserOperationClaim : Core.Security.Domain.Entities.UserOperationClaim
    {
        public virtual User User { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
    }
}
