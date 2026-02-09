using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.ProductDetailServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("Admin/ProductDetail")]
    public class ProductDetailController : BaseController
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductDetailService _productDetailService;
        private readonly IValidator<UpdateProductDetailDto> _updateValidator;
        private readonly IValidator<CreateProductDetailDto> _createValidator;

        public ProductDetailController(IHttpClientFactory httpClientFactory, IProductDetailService productDetailService, IValidator<UpdateProductDetailDto> updateValidator, IValidator<CreateProductDetailDto> createValidator)
        {
            _httpClientFactory = httpClientFactory;
            _productDetailService = productDetailService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }

        [HttpGet]
        [Route("UpdateProductDetail/{id}")]
        public async Task<IActionResult> UpdateProductDetail(string id)
        {
            ProductDetailViewbagList();
            var result = await _productDetailService.GetByProductIdProductDetailAsync(id);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            if (result.Data == null)
            {

                return View(new ProductDetailIndexViewModel
                {
                    CreateProductDetail = new CreateProductDetailDto { ProductId = id },
                });
            }
            ViewBag.ProductImageId = result.Data.ProductDetailID;
            return View(new ProductDetailIndexViewModel
            {
                UpdateProductDetail = result.Data,
            });

        }

        [Route("CreateProductDetail")]
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createProductDetailDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createProductDetailDto);


            }
            var result = await _productDetailService.CreateProductDetailAsync(createProductDetailDto);


            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createProductDetailDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });

        }
        [Route("UpdateProductDetail")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateProductDetailDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateProductDetailDto);


            }
            var result = await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);


            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateProductDetailDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });

        }
        void ProductDetailViewbagList()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Açıklama ve Bilgi GÜncelleme Sayfası";
        }
    }
}
