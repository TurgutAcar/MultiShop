namespace MultiShop.Shared.Events
{
    public sealed class ProductDeletedEvent : IntegrationEvent
    {
        public string ProductId { get; init; }
      
    }


}
