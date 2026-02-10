using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.WebUI.Services.CatologService.ProductSearchServices;
using MultiShop.WebUI.Services.CatologService.ProductService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial:ViewComponent
    {
        private readonly IProductSearchService _productSearchService;

        public _ProductListComponentPartial(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values =await _productSearchService.GetPagedProductsByCategoryIdAsync(id);
            return View(values.Data);
          
        }
    }
}
