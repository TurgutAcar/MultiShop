using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Dtos.SpecialOfferDtos;
using MultiShop.Catalog.Application.Services.SpecialOfferServices;
using MultiShop.Catalog.Infrastructure.Middlewares;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }
        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            var response = await _specialOfferService.GetAllSpecialOfferAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            var response = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var response = await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var response = await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            var response = await _specialOfferService.DeleteSpecialOfferAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
