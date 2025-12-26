using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public sealed class StockReservedEvent : IStockReservedEvent
    {
        public Guid CorrelationId { get; set; }
    }
}
