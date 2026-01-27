using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.AboutServices
{
    public interface IAboutService
    {
        public Task<Result<List<ResultAboutDto>>> AboutListAsync();
        public Task<Result<string>> CreateAboutAsync(CreateAboutDto createAboutDto);
        public Task<Result<string>> UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        public Task<Result<string>> DeleteAboutAsync(string id);
        public Task<Result<UpdateAboutDto>> GetByIdAboutAsync(string id);
    }
}
