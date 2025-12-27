namespace MultiShop.Checkout.Dto
{
    public class ConfirmCheckoutRequest
    {
        public Guid CorrelationId { get; }
        public string UserId { get; }
        public decimal TotalAmount { get; }
        public string CardNumber { get; }
    }
}
