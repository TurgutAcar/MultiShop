
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;

namespace MultiShop.WebUI.Services.BrandServices
{
    public interface IBrandService
    {
        public Task<List<ResultBrandDto>> BrandListAsync();
        public Task CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task DeleteBrandAsync(string id);    
        public Task<UpdateBrandDto> GetByIdBrandAsync(string id);
       


    }
}
