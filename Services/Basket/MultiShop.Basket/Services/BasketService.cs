using System.Text.Json;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Result<string>> DeleteBasket(string userId)
        {
            await _redisService.GetDb().KeyDeleteAsync(userId);
            return "Sepet silindi.";
        }

        public async Task<Result<BasketTotalDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);
            if (existBasket.IsNullOrEmpty)
            {
                return Result<BasketTotalDto>.Failure("Basket not found");
            }


            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket);
        }

        public async Task<Result<string>> SaveBasket(BasketTotalDto basketTotalDto)
        {
            await _redisService.GetDb().StringSetAsync(basketTotalDto.userId, JsonSerializer.Serialize(basketTotalDto));
            return "Sepetteki değişiklikler kaydedildi.";
        }
    }
}
