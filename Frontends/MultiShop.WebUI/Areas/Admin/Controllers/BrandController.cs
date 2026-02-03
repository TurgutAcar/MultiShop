using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
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
    public class BrandController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBrandService _brandService;
        private readonly IValidator<CreateBrandDto> _validator;

        public BrandController(IHttpClientFactory httpClientFactory, IBrandService brandService, IValidator<CreateBrandDto> validator)
        {
            _httpClientFactory = httpClientFactory;
            _brandService = brandService;
            _validator = validator;
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
            //var result=await _brandService.BrandListAsync();
            //var vm = new BrandIndexViewModel
            //{
            //    Brands = result.Data ?? new(),
            //    IsRateLimited = result.StatusCode == 429,
            //    IsLoading = result.StatusCode == 429
            //};

            //return View(vm);



        }
        [HttpGet]
        [Route("GetBrandListPartial")]
        public async Task<IActionResult> GetBrandListPartial()
        {
            // Ocelot Gateway burada Retry/Circuit Breaker işlemlerini yapar.
            // UI sadece bekler.
            var result = await _brandService.BrandListAsync();

            var vm = new BrandIndexViewModel
            {
                Brands = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            // Dikkat: Index değil, sadece içeriği döneceğiz!
            // Bu sayede sayfa yenilenmeden tablo güncellenir.
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
            ValidationResult validationResult = await _validator.ValidateAsync(createBrandDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createBrandDto);

              
            }

            var result=await _brandService.CreateBrandAsync(createBrandDto);
            if(!result.IsSuccessful)
            {
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = UiMessageMapper.Map(result.Source)
                });
                //ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);
                //ModelState.AddModelError("", UiMessageMapper.Map(result.Source));

                return View(createBrandDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });

            return RedirectToAction("Index", "Brand", new { Area = "Admin" });


        }
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(string id)
        {
            var result = await _brandService.GetByIdBrandAsync(id);
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);
            return View(result.Data ?? new UpdateBrandDto());

        }
        [HttpPost]
        [Route("UpdateBrand/{id}")]
        public async Task<IActionResult> UpdateBrand(UpdateBrandDto updateBrandDto)
        {
            var result = await _brandService.UpdateBrandAsync(updateBrandDto);

            if (!result.IsSuccessful)
            {
                TempData.SetUiMessage(new UiMessage
                {
                    Type = UiMessageType.Error,
                    Message = UiMessageMapper.Map(result.Source)
                });
                //ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);
                return View(updateBrandDto);
            }

            //TempData["UiMessage"] = result.Data;
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "Brand", new { Area = "Admin" });

        }
        [HttpPost]
        [Route("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(string id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            if (!result.IsSuccessful)
            {
                return Json(new
                {
                    success = false,
                    message = UiMessageMapper.Map(result.Source),
                    type = "error"
                });
                //TempData.SetUiMessage(new UiMessage
                //{
                //    Type = UiMessageType.Error,
                //    Message = UiMessageMapper.Map(result.Source)
                //});
                //return View();
            }

            return Json(new
            {
                success = true,
                message = result.Data,
                type = "success"
            });

            //return RedirectToAction("Index", "Brand", new { Area = "Admin" });

        }
    }
}
