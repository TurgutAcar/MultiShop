using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Dtos.CategoryDtos;
using MultiShop.Catalog.Application.Services.CategoryServices;
using MultiShop.Catalog.Infrastructure.Middlewares;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
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
