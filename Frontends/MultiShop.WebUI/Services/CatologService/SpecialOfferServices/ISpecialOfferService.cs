using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;

namespace MultiShop.WebUI.Services.CatologService.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        public Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        public Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        public Task DeleteSpecialOfferAsync(string id);
        public Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        public Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
    }
}
