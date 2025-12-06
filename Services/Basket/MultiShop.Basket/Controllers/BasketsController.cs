using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Basket.Dtos;
using MultiShop.Basket.LoginService;
using MultiShop.Basket.Services;

namespace MultiShop.Basket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketsController(ILoginService loginService,IBasketService basketService)
        {
            _loginService = loginService;
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMyBasketDetail()
        {
            var response = await _basketService.GetBasket(_loginService.GetUserId);
            return StatusCode(response.StatusCode,response);

        }
        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
        {
            var user = User.Claims;
            basketTotalDto.userId = _loginService.GetUserId;
            var response=await _basketService.SaveBasket(basketTotalDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMyBasket()
        {
            var response=await _basketService.DeleteBasket(_loginService.GetUserId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
