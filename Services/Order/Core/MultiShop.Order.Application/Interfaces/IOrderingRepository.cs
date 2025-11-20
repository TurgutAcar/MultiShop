using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Order.Domain.OrderAggregate;

namespace MultiShop.Order.Application.Interfaces
{
    public interface IOrderingRepository
    {
        public List<Ordering> GetOrderingsByUserId(string id);
    }
}
