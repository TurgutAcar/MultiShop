using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.BasketService;
using MultiShop.WebUI.Services.CatologService.ProductService;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MultiShop.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public ShoppingCartController(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            var basketResult = await _basketService.GetBasket();

            ViewBag.InfoMessage = TempData["UiMessage"];

            if (!basketResult.IsSuccessful || basketResult.Data == null)
                return View();
           
            var discountCode = TempData["DiscountCode"];
            var discountRate = TempData["DiscountRate"] != null
                ? Convert.ToDecimal(TempData["DiscountRate"])
                : 0;
             var totalPriceWithTax = basketResult.Data.TotalPrice + basketResult.Data.TotalPrice / 100 * 10;

            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * discountRate);
            var tax = basketResult.Data.TotalPrice / 100 * 10;

            ViewBag.code = discountCode;
            ViewBag.discountRate = discountRate;
            ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;
            ViewBag.total = basketResult.Data.TotalPrice;
            ViewBag.tax = tax;
            ViewBag.totalPriceWithTax = totalPriceWithTax;

            //var total = basketResult.Data.TotalPrice;
            //var tax = total * 0.10m;
            //var totalWithTax = total + tax;
            //var discountedTotal = totalWithTax - (totalWithTax * discountRate / 100);


            return View();
            // var totalPriceWithTax = basketResult.Data.TotalPrice + basketResult.Data.TotalPrice / 100 * 10;

            //            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * DiscountCouponCountRateResult.Data);
            //ViewBag.code = code;
            //ViewBag.discountRate = discountRate;
            //ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

            //ViewBag.directory1 = "Ana Sayfa";
            //ViewBag.directory2 = "Ürünler";
            //ViewBag.directory3 = "Sepetim";
            //var values = await _basketService.GetBasket();
            //ViewBag.total = values.TotalPrice;
            //var totalPriceWithTax = values.TotalPrice + values.TotalPrice / 100 * 10;
            //var tax = values.TotalPrice / 100 * 10;
            //ViewBag.totalPriceWithTax = totalPriceWithTax;
            //ViewBag.tax = tax;
            //return View(values);
        }
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var result=await _productService.GetByIdProductAsync(id);
            if (result.IsSuccessful && result.Data == null)
            {
                ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);
                return View();

            }
            var items = new BasketItemDto
            {
                ProductId = result.Data!.ProductId,
                ProductName = result.Data.ProductName,
                Price = result.Data.ProductPrice,
                Quantity = 1,
                ProductImageUrl= result.Data.ProductImageUrl,
            };
           var basketItemResult= await _basketService.AddBasketItem(items);
            if (result.IsSuccessful && result.Data == null)
            {
                ViewBag.InfoMessage = UiMessageMapper.Map(basketItemResult.Source);
                return View();

            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
          
            return RedirectToAction("Index");
        }
    }
}
