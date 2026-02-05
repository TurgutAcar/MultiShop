using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
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
    public class FeatureController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFeatureService _featureService;
        private readonly IValidator<CreateFeatureDto> _createValidator;
        private readonly IValidator<UpdateFeatureDto> _updateValidator;

        public FeatureController(IHttpClientFactory httpClientFactory, IFeatureService featureService, IValidator<CreateFeatureDto> validator, IValidator<UpdateFeatureDto> updateValidator)
        {
            _httpClientFactory = httpClientFactory;
            _featureService = featureService;
            _createValidator = validator;
            _updateValidator = updateValidator;
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
         
            var result = await _featureService.FeatureListAsync();

            var vm = new FeatureIndexViewModel
            {
                Features = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

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
            ValidationResult validationResult = await _createValidator.ValidateAsync(createFeatureDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createFeatureDto);


            }
            var result=await _featureService.CreateFeatureAsync(createFeatureDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createFeatureDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "Feature", new { Area = "Admin" });

           
        }
        [Route("UpdateFeature/{id}")]
        public async Task<IActionResult> UpdateFeature(string id)
        {

            FeatureViewbagList();
            var result = await _featureService.GetByIdFeatureAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }
        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateFeatureDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateFeatureDto);


            }
            var result = await _featureService.UpdateFeatureAsync(updateFeatureDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateFeatureDto);

            }
            SetUISuccessMessage(result.Data);

            return RedirectToAction("Index", "Feature", new { Area = "Admin" });
          
        }
        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var result=await _featureService.DeleteFeatureAsync(id);
            return Json(result);

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
