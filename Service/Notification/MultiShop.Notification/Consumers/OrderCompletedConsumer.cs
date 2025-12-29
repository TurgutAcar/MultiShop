using MassTransit;
using Microsoft.AspNetCore.SignalR;
using MultiShop.Notification.Hubs;
using MultiShop.Shared.Events.EventInterface;

namespace MultiShop.Notification.Consumers
{
    public class OrderCompletedConsumer(
        IHubContext<CheckoutHub> _hubContext) : IConsumer<INotifyOrderCompletedEvent>
    {
        public async Task Consume(ConsumeContext<INotifyOrderCompletedEvent> context)
        {
            await _hubContext.Clients
                .Group(context.Message.UserId)
                .SendAsync("ReceiveNotification", new
                {
                    Type = "SUCCESS",
                    Title = "Sipariş Başarılı",
                    Message = $"Siparişiniz {context.Message.CorrelationId} koduyla onaylandı.",
                    CorrelationId = context.Message.CorrelationId
                });
        }
    }
}
