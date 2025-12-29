namespace MultiShop.Checkout.Dto
{
    public class ConfirmCheckoutRequest
    {
        public Guid CorrelationId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string CardNumber { get; set; }
    }
}
