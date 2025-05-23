using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatologService.FeatureSliderServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CarauselDefaultComponentPartial:ViewComponent
    {
        private readonly IFeatureSliderService _featureSliderService;


        public _CarauselDefaultComponentPartial(IFeatureSliderService featureSliderService)
        {
            this._featureSliderService = featureSliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values=await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
          
        }
    }
}
