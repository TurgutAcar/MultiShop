
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Services.OfferDiscountServices
{
    public interface IOfferDiscountService
    {
        public Task<List<ResultOfferDiscountDto>> OfferDiscountListAsync();
        public Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto);
        public Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto);
        public Task DeleteOfferDiscountAsync(string id);
        public Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id);
    }
}
