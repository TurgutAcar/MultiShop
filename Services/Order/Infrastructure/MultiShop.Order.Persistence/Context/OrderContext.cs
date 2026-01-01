
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;

namespace MultiShop.Order.Persistence.Context
{
    public class OrderContext:DbContext,IUnitOfWork
    {
        public OrderContext(DbContextOptions<OrderContext> options) :base(options)
        {

        }
        public DbSet<Address>  Addresses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ordering>Orderings { get; set; }

    }
}
