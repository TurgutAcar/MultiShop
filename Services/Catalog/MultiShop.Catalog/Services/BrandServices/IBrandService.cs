using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Services.BrandServices
{
    public interface IBrandService
    {
        public Task<List<ResultBrandDto>> BrandListAsync();
        public Task CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task DeleteBrandAsync(string id);    
        public Task<GetByIdBrandDto> GetByIdBrandAsync(string id);
       


    }
}
