using MultiShop.DtoLayer.BasketDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BasketService
{
    public class BasketService : IBasketService
    {
        //private readonly HttpClient _httpClient;
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public BasketService(IApiClientFactory factory,IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {

            var values = await GetBasket();
            if(values!=null)
            {
                if(!values.BasketItems.Any(x=>x.ProductId== basketItemDto.ProductId))
                {
                    values.BasketItems.Add(basketItemDto);
                }
                else
                {
                    values=new BasketTotalDto();
                    values.BasketItems.Add(basketItemDto);
                }
            }
            await SaveBasket(values);
        }

        public Task DeleteBasket(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var _httpClient = _factory.Create("Basket");
            var responseMessage = await _httpClient.GetAsync("baskets");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<BasketTotalDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new BasketTotalDto();
            //var content=await responseMessage.Content.ReadAsStringAsync();
            // var value=JsonConvert.DeserializeObject<BasketTotalDto>(content);
            // return value;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values=await GetBasket();
            var deletedItem=values.BasketItems.FirstOrDefault(x=>x.ProductId==productId);
            var result=values.BasketItems.Remove(deletedItem!);
            await SaveBasket(values);
            return true;
        }

        public async Task<string> SaveBasket(BasketTotalDto basket)
        {
            var _httpClient = _factory.Create("Basket");

            var responseMessage=await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basket);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
