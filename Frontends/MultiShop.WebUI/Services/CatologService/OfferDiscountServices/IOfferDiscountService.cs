
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        public Task<Result<List<ResultOfferDiscountDto>>> OfferDiscountListAsync();
        public Task<string> CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        public Task<string> UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        public Task<string> DeleteOfferDiscountAsync(string id);
        public Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
    }
}
