using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {

        private readonly IDiscountService _discountService;
        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet]
        public async Task<IActionResult> DiscountCouponList() 
        { 
            var response = await _discountService.GetAllDiscountCouponAsync();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
           var response = await _discountService.GetByIdDiscountCouponAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetCodeDetailByCode")]
        public async Task<IActionResult> GetCodeDetailByCode(string code)
        {
            var response = await _discountService.GetCodeDetailByCodeAsync(code);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
        {
            var response = await _discountService.CreateDiscountCouponAsync(createCouponDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
        {
            var response = await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteDiscountCoupon(int couponId)
        {
            var response = await _discountService.DeleteDiscountCouponAsync(couponId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetDiscountCouponCountRate")]
        public async Task<IActionResult> GetDiscountCouponCountRate(string code)
        {
            var response = await _discountService.GetDiscountCouponCountRate(code);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("GetDiscountCouponCount")]
        public async Task<IActionResult> GetDiscountCouponCount()
        {
            var response = await _discountService.GetDiscountCouponCount();
            return StatusCode(response.StatusCode, response);
        }
    }
}
