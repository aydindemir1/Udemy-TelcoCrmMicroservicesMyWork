using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    // Ürünün tipini belirten enum
    // Ürünün yaşam döngüsündeki durumunu belirten enum
    public enum LifecycleStatus
    {
        Planned = 0,  // Planlama aşamasında, henüz satışa çıkmamış ürün
        Active = 1,   // Satışta ve kullanılabilir durumda olan ürün
        Retired = 2,  // Satıştan kaldırılmış, artık yeni müşteri alamayan ürün
        Suspended = 3 // Geçici olarak durdurulmuş, teknik veya ticari sebeplerden satışa kapalı
    }
}
