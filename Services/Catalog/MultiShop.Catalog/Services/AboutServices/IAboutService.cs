using MultiShop.Catalog.Dtos.AboutDtos;

namespace MultiShop.Catalog.Services.AboutServices
{
    public interface IAboutService
    {
        public Task<List<ResultAboutDto>> AboutListAsync();
        public Task CreateAboutAsync(CreateAboutDto createAboutDto);
        public Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        public Task DeleteAboutAsync(string id);
        public Task<GetByIdAboutDto> GetByIdAboutAsync(string id);
    }
}
