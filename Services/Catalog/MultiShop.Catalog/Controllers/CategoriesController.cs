using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Services.CategoryServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Policy = "CatalogReadOrFullPermission")]
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var response = await _categoryService.GetAllCategoryAsync();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var response =await  _categoryService.GetByIdCategoryAsync(id);
            return StatusCode(response.StatusCode, response);  
        }
        [Authorize(Policy = "CatalogFullPermission")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var response=await _categoryService.CreateCategoryAsync(createCategoryDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var response= await _categoryService.DeleteCategoryAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var response=await _categoryService.UpdateCategoryAsync(updateCategoryDto);
            return StatusCode(response.StatusCode, response);
        }

    }
}
