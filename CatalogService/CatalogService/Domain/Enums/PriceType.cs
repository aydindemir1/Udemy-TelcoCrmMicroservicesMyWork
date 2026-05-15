using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    // Fiyat tiplerini temsil eden enum
    public enum PriceType
    {
        Recurring = 0,  // Abonelik gibi tekrar eden ödemeler
        OneTime = 1     // Tek seferlik ödeme
    }
}
