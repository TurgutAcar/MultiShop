using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Services.StatisticService;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("GetBrandCount")]
        public async Task<IActionResult> GetBrandCount()
        {
            var response =await _statisticService.GetBrandCount();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetProductCount")]
        public async Task<IActionResult> GetProductCount()
        {
            var response = await _statisticService.GetProductCount();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetCategoryCount")]
        public async Task<IActionResult> GetCategoryCount()
        {
            var response = await _statisticService.GetCategoryCount();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetProductAvgPrice")]
        public async Task<IActionResult> GetProductAvgPrice()
        {
            var response = await _statisticService.GetProductAvgPrice();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetMaxPriceProductName")]
        public async Task<IActionResult> GetMaxPriceProductName()
        {
            var response = await _statisticService.GetMaxPriceProductName();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetMinPriceProductName")]
        public async Task<IActionResult> GetMinPriceProductName()
        {
            var response = await _statisticService.GetMinPriceProductName();
            return StatusCode(response.StatusCode, response);
        }
    }
}
