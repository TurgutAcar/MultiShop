using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Controllers;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Models;
using MultiShop.WebUI.Services.CatologService.CategoryService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("Admin/Category")]

    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CreateCategoryDto> _validator;

        public CategoryController(ICategoryService categoryService, IValidator<CreateCategoryDto> validator)
        {
            _categoryService = categoryService;
            _validator = validator;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            CategoryViewbagList();
            var vm = new CategoryIndexViewModel
            {
                IsLoading = true,
                Categories = new List<ResultCategoryDto>()
            };
            return View(vm);

           // var values = await _categoryService.GetAllCategoryAsync();
           // return View(values);

        }
        [HttpGet]
        [Route("GetCategoryListPartial")]
        public async Task<IActionResult> GetCategoryListPartial()
        {
            // Ocelot Gateway burada Retry/Circuit Breaker işlemlerini yapar.
            // UI sadece bekler.
            var result = await _categoryService.GetAllCategoryAsync();

            var vm = new CategoryIndexViewModel
            {
                Categories = result.Data ?? new(),
                IsLoading = false // Artık yükleme bitti
            };

            // Dikkat: Index değil, sadece içeriği döneceğiz!
            // Bu sayede sayfa yenilenmeden tablo güncellenir.
            return PartialView("_CategoryContentPartial", vm);
        }
        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            CategoryViewbagList();

            return View();
        }
        [HttpPost]
        [Route("CreateCategory")]

        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(createCategoryDto);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View(createCategoryDto);


            }
            var result=await _categoryService.CreateCategoryAsync(createCategoryDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                //TempData.SetUiMessage(new UiMessage
                //{
                //    Type = UiMessageType.Error,
                //    Message = UiMessageMapper.Map(result.Source)
                //});

                return View(createCategoryDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "Category", new { area = "Admin" });
           
        }
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index", "Category", new { area = "Admin" });

           
        }
        [Route("UpdateCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            CategoryViewbagList();

            var values =await _categoryService.GetByIdCategoryAsync(id);
            return View(values);
           
        }
        [Route("UpdateCategory/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {   
           var result= await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            if (!result.IsSuccessful)
            {
                SetUIErrorMessage(result.ErrorMessages);

                //TempData.SetUiMessage(new UiMessage
                //{
                //    Type = UiMessageType.Error,
                //    Message = UiMessageMapper.Map(result.Source)
                //});

                return View(updateCategoryDto);

            }
            TempData.SetUiMessage(new UiMessage
            {
                Type = UiMessageType.Success,
                Message = result.Data!
            });
            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }
        void CategoryViewbagList()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kategoriler";
            ViewBag.v3 = "Kategori Gücelleme Sayfası";
            ViewBag.v0 = "Kategori İşlemlei";
        }
    }
}
