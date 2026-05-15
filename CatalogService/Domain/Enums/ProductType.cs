using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    // Ürünün tipini belirten enum
    public enum ProductType
    {
        Service = 0, // Hizmet tipi ürün (ör: internet paketi, TV aboneliği)
        Device = 1,  // Fiziksel cihaz (ör: modem, telefon, SIM kart)
        Bundle = 2   // Paket (hizmet + cihaz veya birden fazla hizmet kombinasyonu)
    }
}
