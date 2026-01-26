using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.FeatureService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _FeatureDefaultComponentPartial:ViewComponent
    {
        private IFeatureService _featureService;

        public _FeatureDefaultComponentPartial(IFeatureService featureService)
        {
            _featureService = featureService;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _featureService.FeatureListAsync();
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultFeatureDto>());

        }
    }
}
