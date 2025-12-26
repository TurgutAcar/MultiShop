using MultiShop.Shared.Events.EventInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class OrderCompletedEvent : IOrderCompletedEvent
    {
        public Guid CorrelationId{ get; set; }
    }
}
