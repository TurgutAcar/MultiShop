using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Models
{
    public class BrandIndexViewModel
    {
        public List<ResultBrandDto> Brands { get; set; } = new();
        public bool IsLoading { get; set; }
        public bool IsRateLimited { get; set; }
    }

}
