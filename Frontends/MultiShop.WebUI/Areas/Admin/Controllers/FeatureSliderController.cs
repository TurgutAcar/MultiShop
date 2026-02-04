using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatologService.FeatureSliderServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]
    public class FeatureSliderController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFeatureSliderService _featureSliderService;
        private readonly IValidator<CreateFeatureSliderDto> _validator;

        public FeatureSliderController(IHttpClientFactory httpClientFactory, IFeatureSliderService featureSliderService, IValidator<CreateFeatureSliderDto> validator)
        {
            _httpClientFactory = httpClientFactory;
            _featureSliderService = featureSliderService;
            _validator = validator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            FeatureSliderViewbagList();

            var vm = new FeatureSliderIndexViewModel
            {
                IsLoading = true,
                FeatureSliders = new List<ResultFeatureSliderDto>()
            };
            return View(vm);

        }
        [HttpGet]
        [Route("GetFeatureSliderListPartial")]
        public async Task<IActionResult> GetFeatureSliderListPartial()
        {
           
            var result = await _featureSliderService.GetAllFeatureSliderAsync();

            var vm = new FeatureSliderIndexViewModel
            {
                FeatureSliders = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            // Dikkat: Index değil, sadece içeriği döneceğiz!
            // Bu sayede sayfa yenilenmeden tablo güncellenir.
            return PartialView("_FeatureSliderContentPartial", vm);
        }
       
        [HttpGet]
        [Route("CreateFeatureSlider")]
        public IActionResult CreateFeatureSlider()
        {
            FeatureSliderViewbagList();

            return View();
        }
        [HttpPost]
        [Route("CreateFeatureSlider")]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            createFeatureSliderDto.Status = false;
            ValidationResult validationResult = await _validator.ValidateAsync(createFeatureSliderDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createFeatureSliderDto);


            }
            var result = await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                //TempData.SetUiMessage(new UiMessage
                //{
                //    Type = UiMessageType.Error,
                //    Message = UiMessageMapper.Map(result.Source)
                //});

                return View(createFeatureSliderDto);

            }
            SetUISuccessMessage(result.Data);

            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });

        }
        [HttpGet]
        [Route("UpdateFeatureSlider/{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            FeatureSliderViewbagList();
            var value=await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(value);
          
        }
        [HttpPost]
        [Route("UpdateFeatureSlider/{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var result = await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                //TempData.SetUiMessage(new UiMessage
                //{
                //    Type = UiMessageType.Error,
                //    Message = UiMessageMapper.Map(result.Source)
                //});

                return View(updateFeatureSliderDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });

       
        }
        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        
        }
        void FeatureSliderViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
            ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
        }
    }
}
