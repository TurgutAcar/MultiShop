using MultiShop.Checkout.Messaging;
using MultiShop.Shared.Dtos;

namespace MultiShop.Shared.Events
{
    public sealed class StockReserveRequestedEvent : IntegrationEvent, IStockReserveRequestedEvent
    {
        public Guid CorrelationId { get; set; }
        public List<OrderDetailDto> Items { get; set; }
    }
}
