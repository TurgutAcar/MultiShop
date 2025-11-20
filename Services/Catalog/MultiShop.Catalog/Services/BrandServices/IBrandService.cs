using MultiShop.Catalog.Dtos.BrandDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.BrandServices
{
    public interface IBrandService
    {
        public Task<Result<List<ResultBrandDto>>> BrandListAsync();
        public Task<Result<string>> CreateBrandAsync(CreateBrandDto createBrandDto);
        public Task<Result<string>> UpdateBrandAsync(UpdateBrandDto updateBrandDto);
        public Task<Result<string>> DeleteBrandAsync(string id);    
        public Task<Result<GetByIdBrandDto>> GetByIdBrandAsync(string id);
       


    }
}
