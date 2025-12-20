

using Ardalis.SmartEnum;

namespace MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums
{
    public sealed class StockTransactionType:SmartEnum<StockTransactionType>
    {
        public StockTransactionType(string name, int value) : base(name, value)
        {
        }
        public static readonly ReservationStatus GoodsReceipt = new("Depo girişi", 1);
        public static readonly ReservationStatus OrderReserve = new("Sipariş rezerv", 2);
        public static readonly ReservationStatus OrderRelease = new("Sipariş iptal", 3);
        public static readonly ReservationStatus Fire = new("Kayıp / hasar",4);

      
    }

}
