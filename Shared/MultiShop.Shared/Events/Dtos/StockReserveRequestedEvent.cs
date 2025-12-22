namespace MultiShop.Shared.Events.Dtos
{
    public sealed class StockReserveRequestedEvent : IntegrationEvent
    {
        public int SagaId { get; set; }
        public int OrderId { get; set; }

        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}
