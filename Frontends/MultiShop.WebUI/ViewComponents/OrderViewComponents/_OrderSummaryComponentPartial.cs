using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.BasketService;

namespace MultiShop.WebUI.ViewComponents.OrderViewComponents
{
    public class _OrderSummaryComponentPartial:ViewComponent
    {
        private readonly IBasketService _basketService;

        public _OrderSummaryComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _basketService.GetBasket();

            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new BasketTotalDto());
        }
    }
}
