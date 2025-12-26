using MultiShop.Order.Application.Features.Mediator.Dtos;
using MultiShop.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Shared.Events.EventInterface
{
    public interface IOrderRequestEvent
    {
        public Guid CorrelationId { get; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
        public string OrderNumber { get; set; }
    }
}
