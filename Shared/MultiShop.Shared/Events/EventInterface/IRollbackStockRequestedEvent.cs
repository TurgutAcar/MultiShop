using MultiShop.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.EventInterface
{
    public interface  IRollbackStockRequestedEvent
    {
        Guid CorrelationId { get; }
        List<OrderDetailDto> Items { get; } // set kısmını kaldırabilirsin
    }
}
