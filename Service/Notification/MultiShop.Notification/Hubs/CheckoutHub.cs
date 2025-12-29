using Microsoft.AspNetCore.SignalR;

namespace MultiShop.Notification.Hubs
{
    public class CheckoutHub : Hub
    {
        // Kullanıcı bağlandığında kendini UserId grubuna ekler
        public async Task JoinGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            await Clients.Caller.SendAsync("ReceiveNotification", new
            {
                Message = $"Sistem: {userId} grubuna başarıyla katıldınız. Bildirimler bekleniyor..."
            });
        }
    }
}
