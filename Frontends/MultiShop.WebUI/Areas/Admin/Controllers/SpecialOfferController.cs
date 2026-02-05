using System.Net.Http;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatologService.SpecialOfferServices;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/SpecialOffer")]
    public class SpecialOfferController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISpecialOfferService _specialOfferService;
        private readonly IValidator<CreateSpecialOfferDto> _createValidator;
        private readonly IValidator<UpdateSpecialOfferDto> _updateValidator;
        public SpecialOfferController(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService, IValidator<UpdateSpecialOfferDto> updateValidator, IValidator<CreateSpecialOfferDto> createValidator)
        {
            _httpClientFactory = httpClientFactory;
            _specialOfferService = specialOfferService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            SpecialOfferViewbagList();
            var vm = new SpecialOfferIndexViewModel
            {
                IsLoading = true,
                SpecialOffers = new List<ResultSpecialOfferDto>()
            };
            return View(vm);

        }
        [HttpGet]
        [Route("GetSpecialOfferListPartial")]
        public async Task<IActionResult> GetSpecialOfferListPartial()
        {

            var result = await _specialOfferService.GetAllSpecialOfferAsync();

            var vm = new SpecialOfferIndexViewModel
            {
                SpecialOffers = result.Data ?? new(),
                IsLoading = false
            };


            return PartialView("_ProductContentPartial", vm);
        }
        
        [HttpGet]
        [Route("CreateSpecialOffer")]
        public IActionResult CreateSpecialOffer()
        {
            SpecialOfferViewbagList();

            return View();
        }
        [HttpPost]
        [Route("CreateSpecialOffer")]

        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createSpecialOfferDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createSpecialOfferDto);


            }
            var result = await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createSpecialOfferDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
           
        }
        [Route("UpdateSpecialOffer/{id}")]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            SpecialOfferViewbagList();

            var result = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(result.Data);

          
        }
        [HttpPost]
        [Route("UpdateSpecialOffer/{id}")]

        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateSpecialOfferDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateSpecialOfferDto);


            }
            var result = await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateSpecialOfferDto);

            }
            SetUISuccessMessage(result.Data);
           
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
          
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            var result=await _specialOfferService.DeleteSpecialOfferAsync(id);
            return Json(result);
        }

        void SpecialOfferViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif Ve Günün İndirim Listesi";
            ViewBag.v0 = "Özel Teklif İşlemlei";
        }



    }
}
