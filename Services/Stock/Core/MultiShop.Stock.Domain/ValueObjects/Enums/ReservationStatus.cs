
using Ardalis.SmartEnum;
namespace MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums
{
    public sealed class ReservationStatus:SmartEnum<ReservationStatus>
    {
        public static readonly ReservationStatus Pending = new("Beklemede", 1);
        public static readonly ReservationStatus Confirmed = new("Onaylandı", 2);
        public static readonly ReservationStatus Released = new("Teslim Edildi",3);

        public ReservationStatus(string name,int value) : base(name, value) { }
        
    }

}
