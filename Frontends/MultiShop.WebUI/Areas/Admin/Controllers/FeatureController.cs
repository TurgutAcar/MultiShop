using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.FeatureService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Route("Admin/Feature")]
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFeatureService _featureService;
        private readonly IValidator<CreateFeatureDto> _validator;

        public FeatureController(IHttpClientFactory httpClientFactory, IFeatureService featureService, IValidator<CreateFeatureDto> validator)
        {
            _httpClientFactory = httpClientFactory;
            _featureService = featureService;
            _validator = validator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            FeatureViewbagList();

            var vm = new FeatureIndexViewModel
            {
                IsLoading = true,
                Features = new List<ResultFeatureDto>()
            };
            return View(vm);

        }
        [HttpGet]
        [Route("GetFeatureListPartial")]
        public async Task<IActionResult> GetFeatureListPartial()
        {
            // Ocelot Gateway burada Retry/Circuit Breaker işlemlerini yapar.
            // UI sadece bekler.
            var result = await _featureService.FeatureListAsync();

            var vm = new FeatureIndexViewModel
            {
                Features = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            // Dikkat: Index değil, sadece içeriği döneceğiz!
            // Bu sayede sayfa yenilenmeden tablo güncellenir.
            return PartialView("_FeatureContentPartial", vm);
        }

        [Route("CreateFeature")]
        public IActionResult CreateFeature()
        {
            FeatureViewbagList();

            return View();

        }
        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(createFeatureDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createFeatureDto);


            }
            var result=await _featureService.CreateFeatureAsync(createFeatureDto);
            if (!result.IsSuccessful)
            {
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = UiMessageMapper.Map(result.Source)
                });

                return View(createFeatureDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });

           
        }
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {

            FeatureViewbagList();
            var value=await _featureService.GetByIdFeatureAsync(id);
             return View(value);
        }
        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
           
            var result=await _featureService.UpdateFeatureAsync(updateFeatureDto);
            if (!result.IsSuccessful)
            {
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = UiMessageMapper.Map(result.Source)
                });

                return View(updateFeatureDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });
          
        }
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });
         
        }
        void FeatureViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özellikler";
            ViewBag.v3 = "Özellik Listesi";
            ViewBag.v0 = "Özellik İşlemlei";
        }
    }
}
