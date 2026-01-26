using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CatologService.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        public Task<Result<List<ResultSpecialOfferDto>>> GetAllSpecialOfferAsync();
        public Task<string> CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        public Task<string> DeleteSpecialOfferAsync(string id);
        public Task<string> UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        public Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
    }
}
