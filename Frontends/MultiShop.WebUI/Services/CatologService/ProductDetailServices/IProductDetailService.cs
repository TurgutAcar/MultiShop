
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;

namespace MultiShop.WebUI.Services.ProductDetailServices
{
    public interface IProductDetailService
    {
        Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
        Task<string> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task<string> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task<string> DeleteProductDetailAsync(string id);
        Task<UpdateProductDetailDto> GetByIdProductDetailAsync(string id);
        Task<UpdateProductDetailDto> GetByProductIdProductDetailAsync(string id);



    }
}
