using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.AboutServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAboutService _aboutService;
        private readonly IValidator<CreateAboutDto> _validator;


        public AboutController(IHttpClientFactory httpClientFactory, IAboutService aboutService, IValidator<CreateAboutDto> validator)
        {
            _httpClientFactory = httpClientFactory;
            _aboutService = aboutService;
            _validator = validator;
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
            // Ocelot Gateway burada Retry/Circuit Breaker işlemlerini yapar.
            // UI sadece bekler.
            var result = await _aboutService.AboutListAsync();

            var vm = new AboutIndexViewModel
            {
                Abouts = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            // Dikkat: Index değil, sadece içeriği döneceğiz!
            // Bu sayede sayfa yenilenmeden tablo güncellenir.
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
            ValidationResult validationResult = await _validator.ValidateAsync(createAboutDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createAboutDto);


            }

            var result=await _aboutService.CreateAboutAsync(createAboutDto);
            if (!result.IsSuccessful)
            {
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = UiMessageMapper.Map(result.Source)
                });
             

                return View(createAboutDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "About", new { Area = "Admin" });
         
        }
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            AboutViewbagList();
            var value=await _aboutService.GetByIdAboutAsync(id);
            return View(value);
         
        }
        [HttpPost]
        [Route("UpdateAbout/{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await _aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index", "About", new { Area = "Admin" });
        
        }
        [Route("DeleteAbout/{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await _aboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index", "About", new { Area = "Admin" });
           
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
