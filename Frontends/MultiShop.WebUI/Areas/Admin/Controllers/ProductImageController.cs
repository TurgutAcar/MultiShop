using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.ProductImageServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Route("Admin/ProductImage")]
    public class ProductImageController : BaseController
    {
        private IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [Route("ProductImageDetail/{id}")]
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            ProductImageViewbagList();
            var result = await _productImageService.GetByProductIdProductImageAsync(id);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }
            if(result.Data == null)
            {
             
                return View(new ProductImageIndexViewModel
                {
                    CreateProductImage = new CreateProductImageDto { ProductId = id },
                });
            }
            ViewBag.ProductImageId = result.Data.ProductImageId;
            return View(new ProductImageIndexViewModel
            {
                UpdateProductImage = result.Data,
            });

        }
        [Route("CreateProductImageDetail")]
        [HttpPost]
        public async Task<IActionResult> CreateProductImageDetail(CreateProductImageDto createProductImageDto)
        {
            var result = await _productImageService.CreateProductImageAsync(createProductImageDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createProductImageDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });

          
        }
        [Route("UpdateProductImageDetail")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImageDetail(UpdateProductImageDto updateProductImageDto)
        {
            var result = await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateProductImageDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });


        }
        void ProductImageViewbagList()
        {
            ViewBag.v0 = "Ürün Görsel İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";
        }

    }
}
