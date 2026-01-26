using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.ProductService
{
    public interface IProductService
    {
        Task<Result<List<ResultProductDto>>> GetAllProductAsync();
        Task<string> CreateProductAsync(CreateProductDto createProductDto);
        Task<string> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<string> DeleteProductAsync(string id);
        Task<UpdateProductDto> GetByIdProductAsync(string id);
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId);
    }
}
