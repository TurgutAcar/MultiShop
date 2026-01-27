using MultiShop.DtoLayer.BasketDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public BasketService(IApiClientFactory factory,IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {

            var response = await GetBasket();
            if(response.Data != null)
            {
                if(!response.Data.BasketItems.Any(x=>x.ProductId== basketItemDto.ProductId))
                {
                    response.Data.BasketItems.Add(basketItemDto);
                }
                else
                {
                    response.Data =new BasketTotalDto();
                    response.Data.BasketItems.Add(basketItemDto);
                }
            }
            await SaveBasket(response.Data!);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<BasketTotalDto>> GetBasket()
        {
            var _httpClient = _factory.Create("Basket");
            var response = await _httpClient.GetAsync("baskets");
            return await response.ReadSafeResultAsync<BasketTotalDto>();

        }

        public async Task<Result<string>> RemoveBasketItem(string productId)
        {
            var response=await GetBasket();
            var deletedItem= response.Data!.BasketItems.FirstOrDefault(x=>x.ProductId==productId);
            var result=response.Data.BasketItems.Remove(deletedItem!);
            return await SaveBasket(response.Data);
          
        }

        public async Task<Result<string>> SaveBasket(BasketTotalDto basket)
        {
            var _httpClient = _factory.Create("Basket");

            var response = await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basket);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
