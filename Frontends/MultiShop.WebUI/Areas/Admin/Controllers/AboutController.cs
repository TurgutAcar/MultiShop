using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.AboutServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAboutService _aboutService;
        private readonly IValidator<CreateAboutDto> _createValidator;
        private readonly IValidator<UpdateAboutDto> _updateValidator;


        public AboutController(IHttpClientFactory httpClientFactory, IAboutService aboutService, IValidator<CreateAboutDto> createValidator, IValidator<UpdateAboutDto> updateValidator)
        {
            _httpClientFactory = httpClientFactory;
            _aboutService = aboutService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            AboutViewbagList();

            var vm = new AboutIndexViewModel
            {
                IsLoading = true,
                Abouts = new List<ResultAboutDto>()
            };
            return View(vm);
          
        }
        [HttpGet]
        [Route("GetAboutListPartial")]
        public async Task<IActionResult> GetAboutListPartial()
        {
          
            var result = await _aboutService.AboutListAsync();

            var vm = new AboutIndexViewModel
            {
                Abouts = result.Data ?? new(),
                IsLoading = false 
            };
            return PartialView("_AboutContentPartial", vm);
        }
     
        [Route("CreateAbout")]
        public IActionResult CreateAbout()
        {
            AboutViewbagList();

            return View();
        }
        [HttpPost]
        [Route("CreateAbout")]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createAboutDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createAboutDto);


            }

            var result=await _aboutService.CreateAboutAsync(createAboutDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return View(createAboutDto);
            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "About", new { Area = "Admin" });
         
        }
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            AboutViewbagList();
            var result = await _aboutService.GetByIdAboutAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);
         
        }
        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateAboutDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateAboutDto);


            }
            var result=await _aboutService.UpdateAboutAsync(updateAboutDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return View(updateAboutDto);
            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "About", new { Area = "Admin" });
        
        }
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
           var result= await _aboutService.DeleteAboutAsync(id);
            return Json(result);

            // return RedirectToAction("Index", "About", new { Area = "Admin" });

        }
        void AboutViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Hakkımda";
            ViewBag.v3 = "Hakkımda Listesi";
            ViewBag.v0 = "Hakkımda İşlemleri";
        }
    }
}
