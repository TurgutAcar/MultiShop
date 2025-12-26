using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class StockReservationFailedEvent : IStockReservationFailedEvent
    {
        public Guid CorrelationId { get; set; }

        public string Reason { get; set; }

        public List<FailedStockItem> FailedItems { get; set; }
    }
    public class FailedStockItem
    {
        public string ProductId { get; set; }
        public string Message { get; set; } // Örn: "Stokta sadece 2 adet kaldı."
    }
}
