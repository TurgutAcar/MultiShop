using MultiShop.Shared.Dtos;

namespace MultiShop.Checkout.Dto
{
    public class CheckoutRequestDto
    {
        public string UserId { get; set; }
        public List<OrderDetailDto> Items { get; set; }
        // Adres ID veya Kargo bilgileri gibi ek alanlar buraya gelebilir
        public string AddressId { get; set; }
    }
}
