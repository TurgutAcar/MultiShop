

namespace MultiShop.Services.Stock.Domain.Entities
{
    public class StockItem
    {
        public int Id { get; set; }          
        public string ProductId { get; set; }

        public int TotalQuantity { get; set; }  // Anlık stok

        public int ReservedQuantity { get; set; } // (Opsiyonel ama öneririm)
        public bool IsActive { get; set; } = true;

        public int AvailableQuantity => TotalQuantity - ReservedQuantity;

        public DateTime UpdatedAt { get; set; }
    }

}
