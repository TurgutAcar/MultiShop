using MultiShop.Basket.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Basket.Services
{
    public interface IBasketService
    {
        Task<Result<BasketTotalDto>> GetBasket(string userId);
        Task<Result<string>> SaveBasket(BasketTotalDto basket);
        Task<Result<string>> DeleteBasket(string userId);
    }
}
