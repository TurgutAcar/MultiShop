using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Models
{
    public class ProductDetailIndexViewModel
    {
        public CreateProductDetailDto CreateProductDetail { get; set; } = new();
        public UpdateProductDetailDto UpdateProductDetail { get; set; }

    }

}
