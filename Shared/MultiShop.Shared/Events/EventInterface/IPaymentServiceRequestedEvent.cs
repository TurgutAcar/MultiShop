using MultiShop.Order.Application.Features.Mediator.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public interface IPaymentServiceRequestedEvent
    {
        Guid CorrelationId { get; }
        string UserId { get; }
        decimal TotalAmount { get; }
         string CardNumber { get; }
        public AddressDto Address { get; }


    }
}
