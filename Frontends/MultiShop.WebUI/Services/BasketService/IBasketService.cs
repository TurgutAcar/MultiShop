using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketService
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket();
        Task SaveBasket(BasketTotalDto basket);
        Task DeleteBasket(string userId);
        Task<bool> RemoveBasketItem(string productId);
        Task AddBasketItem(BasketItemDto basketItemDto);


    }
}
