using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Models
{
    public class CategoryIndexViewModel
    {
        public List<ResultCategoryDto> Categories { get; set; } = new();
        public bool IsLoading { get; set; }
        public bool IsRateLimited { get; set; }
    }

}
