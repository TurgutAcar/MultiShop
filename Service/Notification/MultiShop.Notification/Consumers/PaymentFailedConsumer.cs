using MassTransit;
using Microsoft.AspNetCore.SignalR;
using MultiShop.Notification.Hubs;
using MultiShop.Shared.Events;
using MultiShop.Shared.Events.EventInterface;

namespace MultiShop.Notification.Consumers
{
    public class PaymentFailedConsumer(
         IHubContext<CheckoutHub> _hubContext) : IConsumer<INotifyPaymentFailedEvent>
    {
        public async Task Consume(ConsumeContext<INotifyPaymentFailedEvent> context)
        {
            await _hubContext.Clients
                .Group(context.Message.UserId)
                .SendAsync("ReceiveNotification", new
                {
                    Status = "Error",
                    Title = "Ödeme Hatası",
                    Message = "Ödemeniz onaylanmadı, sepetinize geri dönülüyor.",
                    Reason = context.Message.Reason
                });
        }
    }
}
