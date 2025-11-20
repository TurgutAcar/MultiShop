using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Services.ProductImageServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService _productImageService;
        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            var response = await _productImageService.GetAllProductImageAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductImageId(string id)
        {
            var response = await _productImageService.GetByIdProductImageAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("ProductImagesByProductId/{id}")]
        public async Task<IActionResult> ProductImagesByProductId(string id)
        {
            var response = await _productImageService.GetByProductIdProductImageAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            var response = await _productImageService.CreateProductImageAsync(createProductImageDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            var response = await _productImageService.DeleteProductImageAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            var response = await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
