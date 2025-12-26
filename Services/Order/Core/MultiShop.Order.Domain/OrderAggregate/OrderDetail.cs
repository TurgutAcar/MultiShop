using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.OrderAggregate
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public string ProductId { get; set;}
        public string ProductName { get; set;}
        public decimal ProductPrice { get; set;}
        public int ProductAmount { get; set;}
        public decimal ProductTotalPrice { get; set;}
        public int OrderingId { get; set;}
        public Ordering Ordering { get; set;}

        public OrderDetail() { }
        public OrderDetail(string productId, string productName, decimal productPrice, int productAmount, decimal productTotalPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            ProductAmount = productAmount;
            ProductTotalPrice = productTotalPrice;
        }
    }
}
