using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.BasketService;
using MultiShop.WebUI.Services.DiscountServices;

namespace MultiShop.WebUI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            var DiscountCouponCountRateResult = await _discountService.GetDiscountCouponCountRate(code);
            //TempData["UiMessage"] = UiMessageMapper.Map(DiscountCouponCountRateResult.Source);
            if (!DiscountCouponCountRateResult.IsSuccessful)
                return RedirectToAction("Index", "ShoppingCart");


            //var basketResult = await _basketService.GetBasket();
            //    ViewBag.InfoMessage = UiMessageMapper.Map(DiscountCouponCountRateResult.Source);
            //if (!basketResult.IsSuccessful || basketResult.Data == null)
            //{
            //    TempData["UiMessage"] = "Sepet bilgileri alınamadı.";
            //    return RedirectToAction("Index", "ShoppingCart");

            //}
            TempData["DiscountCode"] = code;
            TempData["DiscountRate"] = DiscountCouponCountRateResult.Data;
            // var totalPriceWithTax = basketResult.Data.TotalPrice + basketResult.Data.TotalPrice / 100 * 10;

            //            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * DiscountCouponCountRateResult.Data);
            return RedirectToAction("Index", "ShoppingCart");


        }
        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
           
            return PartialView();
        }
    }
}
