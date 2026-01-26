
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.BrandServices
{
    public interface IBrandService
    {
        public Task<Result<List<ResultBrandDto>>> BrandListAsync();
        public Task<string> CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task<string> UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task<string> DeleteBrandAsync(string id);    
        public Task<UpdateBrandDto> GetByIdBrandAsync(string id);
       


    }
}
