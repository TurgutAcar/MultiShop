using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Stock.Domain.ValueObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Domain.Dtos
{
    public class StockDecreaseRequestDto
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public StockTransactionType Type { get; set; }

        public string? Description { get; set; }
    }

}
