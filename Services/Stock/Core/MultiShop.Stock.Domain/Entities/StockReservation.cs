using MultiShop.Services.Stock.Domain.ValueObjects.Enums;
namespace MultiShop.Services.Stock.Domain.Entities
{
    public class StockReservation
    {
        public int Id { get; set; }

        public string ProductId { get; set; }
        public int Quantity { get; set; }

        public string CartId { get; set; } // SABİT
        public DateTime ExpiresAt { get; set; }

        public ReservationStatus Status { get; set; }
    }

}
