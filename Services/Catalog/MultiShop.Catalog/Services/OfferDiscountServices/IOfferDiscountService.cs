using MultiShop.Catalog.Dtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        public Task<Result<List<ResultOfferDiscountDto>>> OfferDiscountListAsync();
        public Task<Result<string>> CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        public Task<Result<string>> UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        public Task<Result<string>> DeleteOfferDiscountAsync(string id);
        public Task<Result<GetByIdOfferDiscountDto>> GetByIdOfferDiscountAsync(string id);
    }
}
