using MultiShop.Catalog.Dtos.ProductDtos;

namespace MultiShop.Catalog.Services
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(string id);
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync();
        Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId);
    }
}
