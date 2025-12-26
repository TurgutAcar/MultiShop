using MultiShop.Checkout.Dto;
using MultiShop.Checkout.Services;
using MultiShop.Shared.Dtos;

namespace MultiShop.Checkout.Event
{
    public class CheckoutStartedMessage : ICheckoutStarted
    {
        public Guid CorrelationId { get; set; }
        public string UserId { get; set; }
        public List<OrderDetailDto> Items { get; set; }

        public string AddressId { get; set; }
    }
}
