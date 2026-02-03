using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Models
{
    public class AboutIndexViewModel
    {
        public List<ResultAboutDto> Abouts { get; set; } = new();
        public bool IsLoading { get; set; }
        public bool IsRateLimited { get; set; }
    }

}
