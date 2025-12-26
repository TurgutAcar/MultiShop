using MassTransit;
using MultiShop.Order.Application.Features.Mediator.Dtos;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Events;

namespace MultiShop.Checkout.Messaging
{
    public class CheckoutState : SagaStateMachineInstance, ISagaVersion
    {
        public Guid CorrelationId { get; set; } // Siparişi takip eden benzersiz ID
        public string CurrentState { get; set; } // Örn: StockReserved, PaymentPending
        public string UserId { get; set; }
        public List<OrderDetailDto> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? TimeoutTokenId { get; set; } // Zaman aşımı için takip kodu
        public List<FailedStockItem> FailedItems { get; set; }
        public string FailureReason { get; set; }
        public string CardNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public AddressDto Address { get; set; }
        public string OrderNumber { get; set; }
        public int Version { get; set; }
    }
}
