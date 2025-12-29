using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.EventInterface
{
    public interface INotifyOrderCompletedEvent
    {
        Guid CorrelationId { get; }
        string UserId { get; } // Saga bunu kendi state'inden dolduracak
        string Reason { get; }
    }
}
