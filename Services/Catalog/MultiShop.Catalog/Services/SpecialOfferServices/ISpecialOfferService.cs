using MultiShop.Catalog.Dtos.SpecialOfferDtos;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        public Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        public Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        public Task DeleteSpecialOfferAsync(string id);
        public Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        public Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
    }
}
