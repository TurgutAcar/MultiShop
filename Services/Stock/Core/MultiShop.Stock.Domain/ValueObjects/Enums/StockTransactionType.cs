

namespace MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums
{
    public enum StockTransactionType
    {
        GoodsReceipt,   // Depo girişi
        OrderReserve,   // Sipariş rezerv
        OrderRelease,   // Sipariş iptal
        Fire,           // Kayıp / hasar
        ManualAdjust
    }

}
