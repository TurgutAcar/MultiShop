using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Persistence.Context;
namespace MultiShop.Services.Stock.Persistence.Repositories
{
    public class StockTransactionRepository : Repository<StockTransaction, StockContext>,Domain.Repositories.IStockTransactionRepository
    {
        public StockTransactionRepository(StockContext context) : base(context)
        {
        }
    }
}
