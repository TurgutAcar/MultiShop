using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults
{
    public sealed record  GetStockItemByIdQueryResult(
      int Id, 
     string ProductId,
     int TotalQuantity, 
     int ReservedQuantity, 
     DateTime UpdatedAt);
    
}
