using MassTransit;
using MultiShop.Checkout.Dto;
using MultiShop.Shared.Dtos;

namespace MultiShop.Checkout.Services
{
    // Mesajın kendisi
    public interface ICheckoutStarted
    {
        Guid CorrelationId { get; }
        string UserId { get; }
        string AddressId { get; }
        List<OrderDetailDto> Items { get; set; }
    }
}
