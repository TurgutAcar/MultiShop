using MultiShop.Catalog.Dtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        public Task<Result<List<ResultSpecialOfferDto>>> GetAllSpecialOfferAsync();
        public Task<Result<string>> CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        public Task<Result<string>> DeleteSpecialOfferAsync(string id);
        public Task<Result<string>> UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        public Task<Result<GetByIdSpecialOfferDto>> GetByIdSpecialOfferAsync(string id);
    }
}
