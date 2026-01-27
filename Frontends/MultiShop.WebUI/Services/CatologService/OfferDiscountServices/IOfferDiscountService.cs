
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        public Task<Result<List<ResultOfferDiscountDto>>> OfferDiscountListAsync();
        public Task<Result<string>> CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        public Task<Result<string>> UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        public Task<Result<string>> DeleteOfferDiscountAsync(string id);
        public Task<Result<UpdateOfferDiscountDto>> GetByIdOfferDiscountAsync(string id);
    }
}
