
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.BrandServices
{
    public interface IBrandService
    {
        public Task<Result<List<ResultBrandDto>>> BrandListAsync();
        public Task<Result<string>> CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task<Result<string>> UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task<Result<string>> DeleteBrandAsync(string id);    
        public Task<Result<UpdateBrandDto>> GetByIdBrandAsync(string id);
       


    }
}
