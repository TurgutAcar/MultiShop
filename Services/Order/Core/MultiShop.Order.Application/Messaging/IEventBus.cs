namespace MultiShop.Services.Order.Core.Application.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T @event) where T : class;
    }

}
