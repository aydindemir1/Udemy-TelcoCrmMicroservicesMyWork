using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.ProductOfferings.Constants
{
    public static class ProductOfferingMessages
    {
        public const string CannotAddProductToInactive = "Aktif olmayan bir kategoriye ürün eklenemez.";
        public const string CannotCreateOfferFromRetired = "Kullanımdan kaldırılmış bir spesifikasyondan yeni bir teklif oluşturulamaz.";
        public const string ProductOfferingNotFound = "Belirtilen ID'ye sahip bir ürün teklifi bulunamadı.";
    }
}
