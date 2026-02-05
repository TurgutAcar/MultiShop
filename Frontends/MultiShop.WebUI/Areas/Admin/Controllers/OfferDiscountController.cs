using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.OfferDiscountServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/OfferDiscount")]
    public class OfferDiscountController : BaseController
    {
        private IHttpClientFactory _httpClientFactory;
        private IOfferDiscountService _offerDiscountService;
        private readonly IValidator<CreateOfferDiscountDto> _createValidator;
        private readonly IValidator<UpdateOfferDiscountDto> _updateValidator;

        public OfferDiscountController(IHttpClientFactory httpClientFactory, IOfferDiscountService offerDiscountService, IValidator<UpdateOfferDiscountDto> updateValidator, IValidator<CreateOfferDiscountDto> createValidator)
        {
            _httpClientFactory = httpClientFactory;
            _offerDiscountService = offerDiscountService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            OfferDiscountViewbagList();

            var vm = new OfferDiscountViewModel
            {
                IsLoading = true,
                OfferDiscounts = new List<ResultOfferDiscountDto>()
            };
            return View(vm);

        }
        [HttpGet]
        [Route("GetOfferDiscountListPartial")]
        public async Task<IActionResult> GetOfferDiscountListPartial()
        {

            var result = await _offerDiscountService.OfferDiscountListAsync();

            var vm = new OfferDiscountViewModel
            {
                OfferDiscounts = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            return PartialView("_FeatureContentPartial", vm);
        }

      
        [Route("CreateOfferDiscount")]
        public IActionResult CreateOfferDiscount()
        {
            OfferDiscountViewbagList();

            return View();
        }
        [HttpPost]
        [Route("CreateOfferDiscount")]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createOfferDiscountDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createOfferDiscountDto);


            }
            var result = await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createOfferDiscountDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "OfferDiscount", new { Area = "Admin" });
         
        }
        [Route("UpdateOfferDiscount/{id}")]
        public async Task<IActionResult> UpdateOfferDiscount(string id)
        {
            OfferDiscountViewbagList();

            var result = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }
        [Route("UpdateOfferDiscount/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateOfferDiscountDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateOfferDiscountDto);


            }
            var result = await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateOfferDiscountDto);

            }
            SetUISuccessMessage(result.Data);
           
            return RedirectToAction("Index", "OfferDiscount", new { Area = "Admin" });
         
        }
        [Route("DeleteOfferDiscount/{id}")]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            var result=await _offerDiscountService.DeleteOfferDiscountAsync(id);
            return Json(result);

        }
        void OfferDiscountViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan Görseller";
            ViewBag.v3 = "Slider Öne Çıkan Görsel Listesi";
            ViewBag.v0 = "Öne Çıkan Slider Görsel İşlemleri";
        }
    }
}
