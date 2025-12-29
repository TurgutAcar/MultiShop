using MassTransit;
using Microsoft.AspNetCore.SignalR;
using MultiShop.Notification.Hubs;
using MultiShop.Shared.Events;
using MultiShop.Shared.Events.EventInterface;
using System.Text.RegularExpressions;

namespace MultiShop.Notification.Consumers
{
    public class StockReservationFailedConsumer(
        IHubContext<CheckoutHub> _hubContext) : IConsumer<INotifyStockReservationFailedEvent>
    {
       
        public async Task Consume(ConsumeContext<INotifyStockReservationFailedEvent> context)
        {
            // ÖNEMLİ: IStockReservationFailedEvent içinde UserId gelmeli.
            // Eğer gelmiyorsa, Saga içindeki StockReservationFailedEvent fırlatılan yere UserId eklenmeli.
            await _hubContext.Clients.Group(context.Message.UserId).SendAsync("ReceiveNotification", new
            {
                Status = "Error",
                Title = "Stok Hatası",
                Message = "Maalesef bazı ürünler tükendi.",
                Reason = context.Message.Reason
            });
        }
    }
}
