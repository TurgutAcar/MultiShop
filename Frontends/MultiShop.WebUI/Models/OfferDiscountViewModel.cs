using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Models
{
    public class OfferDiscountViewModel
    {
        public List<ResultOfferDiscountDto> OfferDiscounts { get; set; } = new();
        public bool IsLoading { get; set; }
        public bool IsRateLimited { get; set; }
    }

}
