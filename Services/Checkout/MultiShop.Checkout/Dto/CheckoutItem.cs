namespace MultiShop.Checkout.Dto
{
    public class CheckoutItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
