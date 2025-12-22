using MultiShop.Order.Domain.Enums;
using MultiShop.Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.OrderAggregate
{
    public class Ordering:IAggregateRoot
    {
        public int OrderingId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
