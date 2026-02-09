namespace MultiShop.Shared.Events
{
    public sealed class ProductUpdatedEvent : IntegrationEvent
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal ProductPrice { get; init; }
        public string ProductImageUrl { get; set; }

        public string CategoryId { get; init; }
    }


}
