using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum ProductOfferingStatus
    {
        Active = 0,     // Teklif aktif, satışa açık
        Suspended = 1,  // Teklif geçici olarak durdurulmuş
        Retired = 2     // Teklif tamamen satıştan kaldırılmış
    }
}
