using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class PaymentServiceRequestedEvent : IPaymentServiceRequestedEvent
    {
        public Guid CorrelationId { get; init; }

        public string UserId { get; init; }
        public string CardNumber { get; init; }
        public decimal TotalAmount { get; init; }
    }
}
