using Ardalis.SmartEnum;


namespace MultiShop.Order.Domain.Enums
{
    public sealed class OrderStatus : SmartEnum<OrderStatus>
    {
        public static readonly OrderStatus Pending = new("Beklemede", 1);
        public static readonly OrderStatus StockReserved = new("Rezerve", 2);
        public static readonly OrderStatus Paid = new("Ödendi", 3);
        public static readonly OrderStatus Cancelled = new("İptal", 4);

        public OrderStatus(string name, int value) : base(name, value)
        {
        }
    }
}
