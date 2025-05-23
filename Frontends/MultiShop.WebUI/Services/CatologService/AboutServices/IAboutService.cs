using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        public Task<List<ResultAboutDto>> AboutListAsync();
        public Task CreateAboutAsync(CreateAboutDto createAboutDto);
        public Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        public Task DeleteAboutAsync(string id);
        public Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    }
}
