using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public interface IPaymentFailedEvent
    {
        Guid CorrelationId { get; }
        string Reason { get; }
    }
}
