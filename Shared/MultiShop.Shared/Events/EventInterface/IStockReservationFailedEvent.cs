using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public interface IStockReservationFailedEvent
    {
        Guid CorrelationId { get; }
        string Reason { get; }
        // Hangi ürünlerin eksik olduğunu liste olarak dönüyoruz
        List<FailedStockItem> FailedItems { get; }
    }
}
