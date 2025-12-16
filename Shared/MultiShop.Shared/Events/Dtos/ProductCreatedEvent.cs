namespace MultiShop.Shared.Events.Dtos
{
    public class ProductCreatedEvent
    {
        public string ProductId { get; init; }
        public string ProductName { get; init; }
        public decimal ProductPrice { get; init; }
        public string CategoryId { get; init; }
    }


}
