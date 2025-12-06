using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
    public interface IResourceOwnerService
    {
        // JWT claim'lerinden gelen kullanıcı ID'si ve erişilmek istenen kaynak ID'si karşılaştırılır.
        Task<bool> IsResourceOwnerAsync<TResourceID>(
            ClaimsPrincipal user,
            TResourceID resourceId);
    }
}
