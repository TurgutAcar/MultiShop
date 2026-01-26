using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Dtos.ProductDtos;
using MultiShop.Catalog.Application.Services.ProductService;
using MultiShop.Catalog.Infrastructure.Middlewares;
using MultiShop.Shared.Events;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBus _bus;

        public ProductsController(IProductService productService, IBus bus)
        {
            _productService = productService;
            _bus = bus;
        }
        [HttpGet]
        public async Task<IActionResult> ProductList()
        { 
            var response=await _productService.GetAllProductAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductId(string id)
        {
            var response = await _productService.GetByIdProductAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var response = await _productService.CreateProductAsync(createProductDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var response = await _productService.DeleteProductAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var response = await _productService.UpdateProductAsync(updateProductDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("ProductListWithCategory")]
        public async Task<IActionResult> ProductListWithCategory()
        {
            var response =await _productService.GetProductsWithCategoryAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("ProductListWithCategoryByCategoryId/{id}")]
        public async Task<IActionResult> ProductListWithCategoryByCategoryId(string id)
        {
            var response = await _productService.GetProductsWithCategoryByCategoryIdAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("test-publish")]
        public async Task<IActionResult> Test()
        {
            await _bus.Publish(new ProductCreatedEvent
            {
                ProductId = "1",
                ProductName = "Test"
            });

            return Ok("tamam");
        }

    }
}
