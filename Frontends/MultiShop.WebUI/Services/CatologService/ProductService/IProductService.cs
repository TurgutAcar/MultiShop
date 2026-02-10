using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.ProductService
{
    public interface IProductService
    {
        Task<Result<List<ResultProductDto>>> GetAllProductAsync();

        Task<Result<string>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Result<string>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<Result<string>> DeleteProductAsync(string id);
        Task<Result<UpdateProductDto>> GetByIdProductAsync(string id);
        Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryAsync();
        Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId);
    }
}
