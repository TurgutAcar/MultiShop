using MultiShop.Shared.Events.EventInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class NotifyPaymentFailedEvent : INotifyPaymentFailedEvent
    {
        public Guid CorrelationId {get; set; }

        public string UserId  {get; set; }

        public string Reason  {get; set; }
    }
}
