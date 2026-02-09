using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Models
{
    public class ProductImageIndexViewModel
    {
        public CreateProductImageDto CreateProductImage { get; set; } = new();
        public UpdateProductImageDto UpdateProductImage { get; set; }

    }

}
