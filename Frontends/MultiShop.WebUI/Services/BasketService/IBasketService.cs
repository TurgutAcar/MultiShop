using MultiShop.DtoLayer.BasketDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.BasketService
{
    public interface IBasketService
    {
        Task<Result<BasketTotalDto>> GetBasket();
        Task<Result<string>> SaveBasket(BasketTotalDto basket);
        Task DeleteBasket(string userId);
        Task<Result<string>> RemoveBasketItem(string productId);
        Task AddBasketItem(BasketItemDto basketItemDto);


    }
}
