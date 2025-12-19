using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;


namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockTransactionResult
{
    public sealed record GetStockTransactionByIdQueryResult
    {
        public int Id { get; set; }
        public string ProductId { get; set; }

        public int Quantity { get; set; }  

        public StockTransactionType Type { get; set; }

        public int ReferenceId { get; set; } // OrderId, GoodsReceiptId vs

        public DateTime CreatedAt { get; set; }
    }
}
