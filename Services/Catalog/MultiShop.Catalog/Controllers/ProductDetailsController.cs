using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Services;
using MultiShop.Catalog.Services.ProductDetailServices;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService _productDetailService;
        public ProductDetailsController(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            var response = await _productDetailService.GetAllProductDetailAsync();
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var response = await _productDetailService.GetByIdProductDetailAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("GetProductDetailByProductId/{id}")]
        public async Task<IActionResult> GetProductDetailByProductId(string id)
        {
            var response = await _productDetailService.GetByProductIdProductDetailAsync(id);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            var response = await _productDetailService.CreateProductDetailAsync(createProductDetailDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            var response = await _productDetailService.DeleteProductDetailAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            var response = await _productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
