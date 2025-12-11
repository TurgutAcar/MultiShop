using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Services.FeatureService;

namespace MultiShop.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            var response=await _featureService.FeatureListAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]

        public async Task<IActionResult> GetFeatureById(string id)
        {
            var response = await _featureService.GetByIdFeatureAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
             var response = await _featureService.CreateFeatureAsync(createFeatureDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var response = await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            var response = await _featureService.DeleteFeatureAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
