using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Mapping;
using MultiShop.WebUI.Services.CatologService.CategoryService;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.UILayoutViewComponent
{
    public class _NavbarLayoutComponentPartial: ViewComponent
    {
        private ICategoryService _categoryService;

        public _NavbarLayoutComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _categoryService.GetAllCategoryAsync();
         //   ViewBag.InfoMessage = UiMessageMapper.Map(result.Source);

            return View(result.Data ?? new List<ResultCategoryDto>());

        }
    }
}
