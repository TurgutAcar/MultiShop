using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.BrandServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/Brand")]
    public class BrandController :BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBrandService _brandService;
        private readonly IValidator<CreateBrandDto> _createValidator;
        private readonly IValidator<UpdateBrandDto> _updateValidator;

        public BrandController(IHttpClientFactory httpClientFactory, IBrandService brandService, IValidator<CreateBrandDto> createValidator, IValidator<UpdateBrandDto> updateValidator)
        {
            _httpClientFactory = httpClientFactory;
            _brandService = brandService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            var vm = new BrandIndexViewModel
            {
                IsLoading = true,
                Brands = new List<ResultBrandDto>()
            };
            return View(vm);
        }
        [HttpGet]
        [Route("GetBrandListPartial")]
        public async Task<IActionResult> GetBrandListPartial()
        {
          
            var result = await _brandService.BrandListAsync();

            var vm = new BrandIndexViewModel
            {
                Brands = result.Data ?? new(),
                IsLoading = false 
            };
    
            return PartialView("_BrandContentPartial", vm);
        }
        [Route("CreateBrand")]
        public  IActionResult CreateBrand()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateBrand")]
        public async Task<IActionResult> CreateBrand(CreateBrandDto createBrandDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createBrandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createBrandDto);

              
            }

            var result=await _brandService.CreateBrandAsync(createBrandDto);
            if(!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                    return View(createBrandDto);

            }
            SetUISuccessMessage(result.Data);

            return RedirectToAction("Index", "Brand", new { Area = "Admin" });


        }
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            
            var result = await _brandService.GetByIdBrandAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);

        }
        [HttpPost]
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {

            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateBrandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateBrandDto);

            }
            var result = await _brandService.UpdateBrandAsync(updateBrandDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return View(updateBrandDto);

            }
            SetUISuccessMessage(result.Data);

            return RedirectToAction("Index", "Brand", new { Area = "Admin" });

        }
        [HttpPost]
        [Route("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            return Json(result);
          
        }
    }
}
