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
        private readonly IValidator<CreateFeatureSliderDto> _createValidator;
        private readonly IValidator<UpdateFeatureSliderDto> _updateValidator;

        public FeatureSliderController(IHttpClientFactory httpClientFactory, IFeatureSliderService featureSliderService, IValidator<CreateFeatureSliderDto> validator, IValidator<UpdateFeatureSliderDto> updateValidator)
        {
            _httpClientFactory = httpClientFactory;
            _featureSliderService = featureSliderService;
            _createValidator = validator;
            _updateValidator = updateValidator;
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
            ValidationResult validationResult = await _createValidator.ValidateAsync(createFeatureSliderDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createFeatureSliderDto);


            }
            var result = await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createFeatureSliderDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });

        }
        [HttpGet]
        [Route("UpdateFeatureSlider/{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            FeatureSliderViewbagList();
            var result = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }
        [HttpPost]
        [Route("UpdateFeatureSlider/{id}")]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateFeatureSliderDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateFeatureSliderDto);


            }
            var result = await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateFeatureSliderDto);

            }
            SetUISuccessMessage(result.Data);
          
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });

       
        }
        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            var result=await _featureSliderService.DeleteFeatureSliderAsync(id);
            return Json(result);

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
