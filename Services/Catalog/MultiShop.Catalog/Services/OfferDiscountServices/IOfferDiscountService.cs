using MultiShop.Catalog.Dtos.OfferDiscountDtos;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        public Task<List<ResultOfferDiscountDto>> OfferDiscountListAsync();
        public Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        public Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        public Task DeleteOfferDiscountAsync(string id);
        public Task<GetByIdOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
    }
}
