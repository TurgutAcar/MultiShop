namespace MultiShop.Shared.Events.Dtos
{
    public class ProductCreatedEvent
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal ProductPrice { get; init; }
        public string CategoryId { get; init; }
    }


}
