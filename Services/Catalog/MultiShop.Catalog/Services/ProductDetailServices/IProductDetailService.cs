using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<Result<List<ResultProductDetailDto>>> GetAllProductDetailAsync();
        Task<Result<string>> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task<Result<string>> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task<Result<string>> DeleteProductDetailAsync(string id);
        Task<Result<GetByIdProductDetailDto>> GetByIdProductDetailAsync(string id);
        Task<Result<GetByIdProductDetailDto>> GetByProductIdProductDetailAsync(string id);



    }
}
