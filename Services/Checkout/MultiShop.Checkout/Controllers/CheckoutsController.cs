using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Checkout.Dto;
using MultiShop.Checkout.Event;
using MultiShop.Checkout.Messaging;
using MultiShop.Checkout.Services;
using MultiShop.Shared.Events;

namespace MultiShop.Checkout.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CheckoutsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
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
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmCheckout(ConfirmCheckoutRequest request)
        {
            var message = new PaymentServiceRequestedEvent
            {
                CorrelationId = request.CorrelationId, // Saga ile eşleşir
                CardNumber = request.CardNumber,
                TotalAmount = request.TotalAmount,
                Address=request.Address,
                UserId=request.UserId


            };

            await _publishEndpoint.Publish<IPaymentServiceRequestedEvent>(message);

            return Ok("Payment requested!");
        }
    }
}
