using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Checkout.Dto;
using MultiShop.Checkout.Event;
using MultiShop.Checkout.Messaging;
using MultiShop.Checkout.Services;

namespace MultiShop.Checkout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CheckoutsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public CheckoutsController()
        {
        }

        [HttpPost("pay")]
        public async Task<IActionResult> Pay(CheckoutRequestDto request)
        {
            var message = new CheckoutStartedMessage
            {
                CorrelationId = Guid.NewGuid(),
                UserId = request.UserId,
                Items = request.Items
            };

            await _publishEndpoint.Publish<ICheckoutStarted>(message);

            return Ok();
        }
    }
}
