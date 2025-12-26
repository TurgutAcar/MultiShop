using MultiShop.Shared.Dtos;

namespace MultiShop.Checkout.Messaging
{
    public interface IStockReserveRequestedEvent
    {
        Guid CorrelationId { get; }
        List<OrderDetailDto> Items { get; } // set kısmını kaldırabilirsin
    }
}
