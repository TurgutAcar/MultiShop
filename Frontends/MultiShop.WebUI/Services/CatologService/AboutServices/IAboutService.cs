using MultiShop.DtoLayer.CatalogDtos.AboutDtos;

namespace MultiShop.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        public Task<List<ResultAboutDto>> AboutListAsync();
        public Task<string> CreateAboutAsync(CreateAboutDto createAboutDto);
        public Task<string> UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        public Task<string> DeleteAboutAsync(string id);
        public Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    }
}
