using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Application.Dtos.OfferDiscountDtos;
using MultiShop.Catalog.Application.Services.OfferDiscountServices;
using MultiShop.Catalog.Infrastructure.Middlewares;

namespace MultiShop.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferDiscountsController : ControllerBase
    {
        private IOfferDiscountService _offerDiscountService;

        public OfferDiscountsController(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }
        [HttpGet]
        public async Task<IActionResult> OfferDiscountList()
        {
            var response=await _offerDiscountService.OfferDiscountListAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferDiscountById(string id)
        {
            var response = await _offerDiscountService.GetByIdOfferDiscountAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOfferDiscount(UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            var response = await _offerDiscountService.UpdateOfferDiscountAsync(updateOfferDiscountDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOfferDiscount(CreateOfferDiscountDto createOfferDiscountDto)
        {
            var response = await _offerDiscountService.CreateOfferDiscountAsync(createOfferDiscountDto);

            return StatusCode(response.StatusCode, response);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOfferDiscount(string id)
        {
            var response = await _offerDiscountService.DeleteOfferDiscountAsync(id);

            return StatusCode(response.StatusCode, response);

        }
    }
}
