using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatologService.CategoryService;
using MultiShop.WebUI.Services.CatologService.ProductService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/Product")]
    public class ProductController : BaseController
    {
      
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;
        public ProductController(IHttpClientFactory httpClientFactory, IProductService productService, ICategoryService categoryService, IValidator<UpdateProductDto> updateValidator, IValidator<CreateProductDto> createValidator)
        {
            _httpClientFactory = httpClientFactory;
            _productService = productService;
            _categoryService = categoryService;
            _updateValidator = updateValidator;
            _createValidator = createValidator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            ProductViewbagList();
            var vm = new ProductIndexViewModel
            {
                IsLoading = true,
                Products = new List<ResultProductDto>()
            };
            return View(vm);
           
        }
        [HttpGet]
        [Route("GetProductListPartial")]
        public async Task<IActionResult> GetProductListPartial()
        {

            var result = await _productService.GetAllProductAsync();

            var vm = new ProductIndexViewModel
            {
                Products = result.Data ?? new(),
                IsLoading = false 
            };


            return PartialView("_ProductContentPartial", vm);
        }
        [HttpGet]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct()
        {
            ProductViewbagList();
            

            var client =_httpClientFactory.CreateClient();
            var values = await _categoryService.GetAllCategoryAsync();
            List<SelectListItem> categoryValues = (from x in values.Data
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;
            return View();
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            ValidationResult validationResult = await _createValidator.ValidateAsync(createProductDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createProductDto);


            }
            var result = await _productService.CreateProductAsync(createProductDto);

            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(createProductDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "Product", new { area = "Admin" });

          
        }
        [Route("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var result=await _productService.DeleteProductAsync(id);
            return Json(result);

        }


        [HttpGet]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            ProductViewbagList();

            var result = await _categoryService.GetAllCategoryAsync();
            if (!result.IsSuccessful && result.Data == null)
            {
                SetUIErrorMessage(result.ErrorMessages);
                return RedirectToAction("Index");
            }

            List<SelectListItem> categoryValues = (from x in result.Data
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryId
                                                   }).ToList();
            ViewBag.CategoryValues = categoryValues;


            var resultGetByIdProduct = await _productService.GetByIdProductAsync(id);
            if (!resultGetByIdProduct.IsSuccessful && resultGetByIdProduct.Data == null)
            {
                SetUIErrorMessage(resultGetByIdProduct.ErrorMessages);
                return RedirectToAction("Index");
            }
            return View(resultGetByIdProduct.Data);
          
        }
        [HttpPost]
        [Route("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            ValidationResult validationResult = await _updateValidator.ValidateAsync(updateProductDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(updateProductDto);


            }
            var result = await _productService.UpdateProductAsync(updateProductDto);


            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                return View(updateProductDto);

            }
            SetUISuccessMessage(result.Data);
            return RedirectToAction("Index", "Product", new { area = "Admin" });

          
        }
        [Route("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            ProductViewbagList();
            var vm = new ProductsWithCategoryIndexViewModel
            {
                IsLoading = true,
                Products = new List<ResultProductsWithCategoryDto>()
            };
            return View(vm);
          
        }
        [HttpGet]
        [Route("GetProductListWithCategoryPartial")]
        public async Task<IActionResult> GetProductListWithCategoryPartial()
        {

            var result = await _productService.GetProductsWithCategoryAsync();

            var vm = new ProductsWithCategoryIndexViewModel
            {
                Products = result.Data ?? new(),
                IsLoading = false
            };


            return PartialView("_ProductListWithCategoryPartial", vm);
        }
       
        void ProductViewbagList()
        {
            ViewBag.v0 = "Ürün İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürünler";
            ViewBag.v3 = "Ürün Listesi";
        }


    }
}
