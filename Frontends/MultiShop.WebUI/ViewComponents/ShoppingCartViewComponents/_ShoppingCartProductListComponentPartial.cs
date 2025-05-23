using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.BasketService;
using MultiShop.WebUI.Services.CatologService.ProductService;

namespace MultiShop.WebUI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartProductListComponentPartial:ViewComponent
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;

        public _ShoppingCartProductListComponentPartial(IBasketService basketService, IProductService productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

    
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _basketService.GetBasket();
            var basketItems=values.BasketItems;
            return View(basketItems);
        }
    }
}
