
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
namespace MultiShop.Services.Stock.Domain.Entities
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public string ProductId { get; set; }

        public int Quantity { get; set; }  // + / -

        public StockTransactionType Type { get; set; }

        public int ReferenceId { get; set; } // OrderId, GoodsReceiptId vs
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
