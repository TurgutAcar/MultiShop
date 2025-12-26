using MultiShop.Order.Application.Features.Mediator.Dtos;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Events.EventInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events
{
    public class OrderRequestEvent : IOrderRequestEvent
    {
        public Guid CorrelationId{ get; set; }

        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
        public string OrderNumber { get; set; }
    }
}
