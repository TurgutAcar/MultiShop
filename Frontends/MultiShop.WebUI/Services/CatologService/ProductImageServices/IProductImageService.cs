
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Services.ProductImageServices
{
    public interface IProductImageService
    {
        public Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        public Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        public Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        public Task DeleteProductImageAsync(string id);
        public Task<UpdateProductImageDto> GetByIdProductImageAsync(string id);
        public Task<UpdateProductImageDto> GetByProductIdProductImageAsync(string id);

    }
}
