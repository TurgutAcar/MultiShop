using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    public class ResourceOwnerService : IResourceOwnerService
    {
        private readonly IRepository<Ordering> _orderRepository;

        // IOrderRepository (sizin MultiShop.Order.Application/Interfaces klasörünüzde olmalı)
        public ResourceOwnerService(IRepository<Ordering> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> IsResourceOwnerAsync<TResourceID>(
            ClaimsPrincipal user,
            TResourceID resourceId)
        {
            // 1. JWT'den Kullanıcı ID'sini Çekin
            // 'sub' (subject) claim'i genellikle kullanıcı ID'sini tutar.
           // var userIdClaim = user.FindFirst("sub");
            var userIdClaim = user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value)) return false;

            // Claim değeri string'dir, kendi kullanıcı ID tipinize dönüştürün.
            // Örn: Eğer kullanıcı ID'niz Guid ise:
            var currentUserId = userIdClaim.Value;
           

            // 2. Kaynağı (Order) Veritabanından Çekin ve Sahibi Kontrol Edin
            // TResourceID'nin int olduğunu varsayıyoruz (OrderId).
            if (resourceId is int orderId)
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                if (order == null)
                {
                    // Kaynak bulunamazsa, güvenlik açısından izin vermemek mantıklı olabilir.
                    // Veya Controller'ın 404 döndürmesini sağlamak için burada 'true' da dönebiliriz (tartışmalı).
                    // Şimdilik 404'ü Controller'a bırakıp başarılı sayalım.
                    return true;
                }

                // 3. Karşılaştırma
                return order.UserId == currentUserId;
            }

            // Beklenmeyen Kaynak ID tipi
            return false;
        }
    }
}
