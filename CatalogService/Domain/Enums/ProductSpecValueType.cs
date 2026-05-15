using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public enum ProductSpecValueType
    {
        String = 1,
        // .NET: string
        // DB: nvarchar(max) veya nvarchar(255)
        // Açıklama: Metinsel veriler (örn. "Kırmızı", "XL")

        Integer = 2,
        // .NET: int
        // DB: int
        // Açıklama: Tam sayılar (örn. 42, 1000)

        Decimal = 3,
        // .NET: decimal
        // DB: decimal(18,2)
        // Açıklama: Ondalıklı sayılar (örn. 99.99, 1200.50)

        Boolean = 4,
        // .NET: bool
        // DB: bit
        // Açıklama: Mantıksal değerler (true/false)

        DateTime = 5,
        // .NET: DateTime
        // DB: datetime2
        // Açıklama: Tarih ve saat bilgisi (örn. 2025-08-12 14:30:00)
    }
}
