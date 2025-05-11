using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        public Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        public Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        public Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        public Task DeleteProductImageAsync(string id);
        public Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id);
    }
}
