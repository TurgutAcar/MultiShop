using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.CatologService.ProductService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureProductDefaultComponentPartial:ViewComponent
    {
        private readonly IProductService _productService;

        public _FeatureProductDefaultComponentPartial(IProductService productService)
        {
            _productService = productService;
        }
       

          
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _productService.GetAllProductAsync();
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultProductDto>());
        }
    }
}
