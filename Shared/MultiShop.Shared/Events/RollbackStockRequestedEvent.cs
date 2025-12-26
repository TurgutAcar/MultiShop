using MultiShop.Shared.Dtos;
using MultiShop.Shared.Events.EventInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class RollbackStockRequestedEvent : IRollbackStockRequestedEvent
    {
        public Guid CorrelationId {get;set;}

        public List<OrderDetailDto> Items { get; set; }
    }
}
