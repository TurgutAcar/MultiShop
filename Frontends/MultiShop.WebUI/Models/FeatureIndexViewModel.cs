using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Models
{
    public class FeatureIndexViewModel
    {
        public List<ResultFeatureDto> Features { get; set; } = new();
        public bool IsLoading { get; set; }
        public bool IsRateLimited { get; set; }
    }

}
