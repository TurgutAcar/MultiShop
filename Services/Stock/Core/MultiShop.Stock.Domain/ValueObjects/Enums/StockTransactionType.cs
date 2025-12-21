

using Ardalis.SmartEnum;

namespace MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums
{
    public sealed class StockTransactionType:SmartEnum<StockTransactionType>
    {
        public StockTransactionType(string name, int value) : base(name, value)
        {
        }
        public static readonly StockTransactionType GoodsReceipt = new("Depo girişi", 1);
        public static readonly StockTransactionType OrderReserve = new("Sipariş rezerv", 2);
        public static readonly StockTransactionType OrderRelease = new("Sipariş iptal", 3);
        public static readonly StockTransactionType Fire = new("Kayıp / hasar",4);
        public static readonly StockTransactionType Return = new("İade", 5);


    }

}
