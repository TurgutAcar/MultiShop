
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Search.Services.ProductSearch;

namespace MultiShop.Catalog.Search.Controllers
{
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
        [ResponseCache(Duration = 30)]
        public async Task<IActionResult> SearchProductsList(string categoryId, int page = 1,
            int pageSize = 10,
            double lastPrice = 0,
            string lastId = null,
            string sortField = "productPrice",
            string sortOrder = "asc")
        {
            var response = await _productSearchService.SearchProductsAsync(categoryId, pageSize, lastPrice, lastId, sortField, sortOrder);
            return StatusCode(response.StatusCode, response);

        }
        //public async Task<IActionResult> SearchProductsList(string categoryId,int page = 1,
        //    int pageSize = 10,
        //    string sortField = "productPrice",
        //    string sortOrder = "asc")
        //{
        //    var response =  await _productSearchService.SearchProductsAsync(categoryId, page, pageSize, sortField, sortOrder);
        //    return StatusCode(response.StatusCode, response);

        //}
        [HttpGet("search")]
        [ResponseCache(Duration = 30)]

        public async Task<IActionResult> Search([FromQuery] string query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {

            var response = await _productSearchService.SearchProductNameAsync(query, page, pageSize);
            return StatusCode(response.StatusCode, response);

        }
    }
}
