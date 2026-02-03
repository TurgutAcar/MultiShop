using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.AboutServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponent
{
    public class _FooterUILayoutComponentPartial:ViewComponent
    {
        private IAboutService _aboutService;

        public _FooterUILayoutComponentPartial(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

           
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _aboutService.AboutListAsync();
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultAboutDto>());
        }
    }
}
