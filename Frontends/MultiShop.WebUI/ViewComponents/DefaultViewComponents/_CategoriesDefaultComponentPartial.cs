using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.CatologService.CategoryService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultComponentPartial:ViewComponent
    {
        
            private ICategoryService _categoryService;

        public _CategoriesDefaultComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await  _categoryService.GetAllCategoryAsync();
            ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultCategoryDto>());


        }     
    }
}
