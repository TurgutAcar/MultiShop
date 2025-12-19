using Microsoft.EntityFrameworkCore;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Services.Stock.Persistence.Context;

namespace MultiShop.Services.Stock.Persistence.Repositories
{
    public class StockItemRepository : Repository<StockItem,StockContext>, IStockItemRepository
    {
        public StockItemRepository(StockContext context) : base(context)
        {
        }
    }
}
