
using Microsoft.EntityFrameworkCore;
using MultiShop.Services.Stock.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
namespace MultiShop.Services.Stock.Persistence.Context
{
    public class StockContext : DbContext,IUnitOfWork
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {

        }
        public DbSet<StockItem> Stocks { get; set; }
        public DbSet<StockReservation> StockReservations { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

           
        }
    }
}
