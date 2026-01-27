
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.ProductImageServices
{
    public interface IProductImageService
    {
        public Task<Result<List<ResultProductImageDto>>> GetAllProductImageAsync();
        public Task<Result<string>> CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        public Task<Result<string>> UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        public Task<Result<string>> DeleteProductImageAsync(string id);
        public Task<Result<UpdateProductImageDto>> GetByIdProductImageAsync(string id);
        public Task<Result<UpdateProductImageDto>> GetByProductIdProductImageAsync(string id);

    }
}
