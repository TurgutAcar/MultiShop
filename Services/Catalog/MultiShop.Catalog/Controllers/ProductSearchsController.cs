
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Services.ProductSearch;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSearchsController : ControllerBase
    {
        private readonly IProductSearchService _productSearchService;
        public ProductSearchsController(IProductSearchService productSearchService)
        {
            _productSearchService = productSearchService;
        }
        [HttpGet("sort")]
        public async Task<IActionResult> SearchProductsList(int page = 1,
            int pageSize = 10,
            string sortField = "productPrice",
            string sortOrder = "asc")
        {
            var response =  await _productSearchService.SearchProductsAsync(page, pageSize, sortField, sortOrder);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var response = await _productSearchService.SearchProductNameAsync(query, page, pageSize);
            return StatusCode(response.StatusCode, response);

        }
    }
}
