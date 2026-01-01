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
        //public OrderStatus Status { get; set; }

        public DateTime OrderDate { get; set; }
        public Address Address { get; set; }
        private readonly List<OrderDetail> _orderDetails;
        public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;

        public Ordering() { }

        public Ordering(string userId, decimal totalPrice,DateTime orderDate, Address address)
        {
            UserId = userId;
            TotalPrice = totalPrice;
           // Status = OrderStatus.FromValue(1);
            OrderDate = DateTime.Now;
            Address = address;
            _orderDetails = new List<OrderDetail>();
        }
        public void AddOrderDetail(string productId, string productName, decimal productPrice, int productAmount, decimal productTotalPrice)
        {
            var existProduct=_orderDetails.Any(od => od.ProductId == productId);
            if(!existProduct)
            {
                var orderDetail = new OrderDetail(productId, productName, productPrice, productAmount, productTotalPrice);
                _orderDetails.Add(orderDetail);
            }
               
        }
    }
}
