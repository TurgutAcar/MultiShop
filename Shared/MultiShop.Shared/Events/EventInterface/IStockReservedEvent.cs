using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public interface  IStockReservedEvent
    {
        Guid CorrelationId { get; }
    }
}
