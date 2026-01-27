
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<Result<List<ResultProductDetailDto>>> GetAllProductDetailAsync();
        Task<Result<string>> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task<Result<string>> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task<Result<string>> DeleteProductDetailAsync(string id);
        Task<Result<UpdateProductDetailDto>> GetByIdProductDetailAsync(string id);
        Task<Result<UpdateProductDetailDto>> GetByProductIdProductDetailAsync(string id);



    }
}
