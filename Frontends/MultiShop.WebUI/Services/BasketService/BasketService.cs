using MultiShop.DtoLayer.BasketDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var responseMessage = await _httpClient.GetAsync("baskets");
            var content=await responseMessage.Content.ReadAsStringAsync();
            var value=JsonConvert.DeserializeObject<BasketTotalDto>(content);
            return value;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values=await GetBasket();
            var deletedItem=values.BasketItems.FirstOrDefault(x=>x.ProductId==productId);
            var result=values.BasketItems.Remove(deletedItem!);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basket)
        {
            await _httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basket);   
        }
    }
}
