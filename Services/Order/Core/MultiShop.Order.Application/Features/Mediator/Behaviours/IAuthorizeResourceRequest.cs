using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Behaviours
{
    public interface IAuthorizeResourceRequest<TResourceID>
    {
        // Kaynağın ID'sini almak için bir özellik tanımlayın.
        // Örn: GetOrderingQueryById'de bu OrderId olur.
        TResourceID ResourceId { get; set; }
    }
}
