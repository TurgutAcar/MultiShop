
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Persistence.Context;

namespace MultiShop.Order.Persistence.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderContext _orderContext;

        public OrderingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }

        public List<Ordering> GetOrderingsByUserId(string id)
        {
            // var values=_orderContext.Orderings.Where(x => x.UserId == id).ToList();
            // return values;
                    return  _orderContext.Orderings
                 .Include(x => x.OrderDetails)
                 .Where(x => x.UserId == id)
                 .Include(x => x.Address)
                 .Where(x => x.UserId == id)
                 .ToList();

        }


    }
}
