using MultiShop.Catalog.Application.Dtos.ProductDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.ProductService
{
    public interface IProductService
    {
        Task<Result<List<ResultProductDto>>> GetAllProductAsync();
        Task<Result<string>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Result<string>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<Result<string>> DeleteProductAsync(string id);
        Task<Result<GetByIdProductDto>> GetByIdProductAsync(string id);
        Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryAsync();
        Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId);
    }
}
