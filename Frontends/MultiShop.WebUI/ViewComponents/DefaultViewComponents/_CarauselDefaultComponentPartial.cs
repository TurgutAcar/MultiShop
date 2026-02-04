using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
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
            var result = await _featureSliderService.GetAllFeatureSliderAsync();
            //ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultFeatureSliderDto>());
          
        }
    }
}
