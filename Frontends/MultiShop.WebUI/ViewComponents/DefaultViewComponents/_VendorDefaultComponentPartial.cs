using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.BrandServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultComponentPartial:ViewComponent
    {
        private IBrandService _brandService;

        public _VendorDefaultComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }
     
           
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _brandService.BrandListAsync();
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultBrandDto>());
        }
    }
}
