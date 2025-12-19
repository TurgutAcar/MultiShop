using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Services.Stock.Persistence.Context;
using MultiShop.Services.Stock.Persistence.Repositories;
using MultiShop.Stock.Domain.Repositories;


namespace MultiShop.Stock.Persistence.Repositories
{
 
    public class StockReservationRepository : Repository<StockReservation, StockContext>, IStockReservationRepository
    {
        public StockReservationRepository(StockContext context) : base(context)
        {
        }
    }
}
