using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        public Task<Result<List<ResultProductImageDto>>> GetAllProductImageAsync();
        public Task<Result<string>> CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        public Task<Result<string>> UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        public Task<Result<string>>  DeleteProductImageAsync(string id);
        public Task<Result<GetByIdProductImageDto>> GetByIdProductImageAsync(string id);
        public Task<Result<GetByIdProductImageDto>>GetByProductIdProductImageAsync(string id);

    }
}
