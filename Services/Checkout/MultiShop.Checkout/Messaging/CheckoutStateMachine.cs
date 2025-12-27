using MassTransit;
using MultiShop.Checkout.Event;
using MultiShop.Checkout.Services;
using MultiShop.Shared.Events;
using MultiShop.Shared.Events.EventInterface;

namespace MultiShop.Checkout.Messaging
{
    public class CheckoutStateMachine : MassTransitStateMachine<CheckoutState>
    {
        public CheckoutStateMachine()
        {
            // 1. Durumun nerede tutulacağını belirtiyoruz
            InstanceState(x => x.CurrentState);

            // 2. Eventlerin CorrelationID ile eşleştirilmesi
            Event(() => CheckoutStartedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => StockReservedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => StockReservationFailedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => PaymentRequestedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => PaymentCompletedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => PaymentFailedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));
            Event(() => OrderCompletedEvent, x => x.CorrelateById(ctx => ctx.Message.CorrelationId));

            // 3. Akış Tanımı
            Initially(
                When(CheckoutStartedEvent)
                    .Then(ctx =>
                    {
                        ctx.Saga.UserId = ctx.Message.UserId;
                        ctx.Saga.Items = ctx.Message.Items;
                        ctx.Saga.CreatedAt = DateTime.UtcNow;
                    })
                    .Publish(ctx => new StockReserveRequestedEvent
                    {
                        CorrelationId = ctx.Saga.CorrelationId,
                        Items = ctx.Saga.Items
                    })
                    .TransitionTo(StockReservedState)
            );

            // STOK BEKLERKEN GELEN CEVAPLAR
            During(StockReservedState,
                When(StockReservedEvent)
                    .Then(ctx => Console.WriteLine($"Sipariş {ctx.Saga.CorrelationId} için stok rezerve edildi."))
                    .TransitionTo(StockReservedState), // burada sadece beklemeye devam ediyor

                When(StockReservationFailedEvent)
                    .Then(ctx =>
                    {
                        ctx.Saga.FailureReason = ctx.Message.Reason;
                        ctx.Saga.FailedItems = ctx.Message.FailedItems;
                    })
                    .TransitionTo(CancelledState)
            );

            // Kullanıcı ödeme/adres bilgilerini girip "Siparişi Onayla" dediğinde PaymentRequestedEvent gelir
            During(StockReservedState,
                When(PaymentRequestedEvent)
                    .Then(ctx =>
                    {
                        ctx.Saga.CardNumber = ctx.Message.CardNumber;
                        ctx.Saga.TotalAmount = ctx.Message.TotalAmount;
                    })
                    .Publish(ctx => new PaymentServiceRequestedEvent
                    {
                        CorrelationId = ctx.Saga.CorrelationId,
                        UserId = ctx.Saga.UserId,
                        CardNumber = ctx.Saga.CardNumber,
                        TotalAmount = ctx.Saga.TotalAmount
                    })
                    .TransitionTo(PaymentPendingState)
            );

            During(PaymentPendingState,
                When(PaymentCompletedEvent)
                    .Then(ctx => Console.WriteLine($"Ödeme tamamlandı, sipariş oluşturuluyor..."))
                    .Publish(ctx => new OrderRequestEvent
                    {
                        CorrelationId = ctx.Saga.CorrelationId,
                        UserId = ctx.Saga.UserId,
                        TotalPrice = ctx.Saga.TotalAmount,
                        OrderDate = ctx.Saga.OrderDate,
                        OrderItems = ctx.Saga.Items,
                        Address = ctx.Saga.Address,
                        OrderNumber = ctx.Saga.OrderNumber
                    })
                    .TransitionTo(OrderCreatingState),

                When(PaymentFailedEvent)
                    .Publish(ctx => new RollbackStockRequestedEvent { CorrelationId = ctx.Saga.CorrelationId })
                    .TransitionTo(CancelledState)
            );

            During(OrderCreatingState,
                When(OrderCompletedEvent)
                    .Then(ctx => Console.WriteLine($"TEBRİKLER! Sipariş {ctx.Saga.CorrelationId} başarıyla tamamlandı."))
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }

        // Event tanımları
        public MassTransit.Event<ICheckoutStarted> CheckoutStartedEvent { get; private set; }
        public MassTransit.Event<IStockReservedEvent> StockReservedEvent { get; private set; }
        public MassTransit.Event<IStockReservationFailedEvent> StockReservationFailedEvent { get; private set; }
        public MassTransit.Event<IPaymentServiceRequestedEvent> PaymentRequestedEvent { get; private set; }
        public MassTransit.Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; private set; }
        public MassTransit.Event<IPaymentFailedEvent> PaymentFailedEvent { get; private set; }
        public MassTransit.Event<IOrderCompletedEvent> OrderCompletedEvent { get; private set; }

        // State tanımları
        public State StockReservedState { get; private set; }
        public State PaymentPendingState { get; private set; }
        public State CancelledState { get; private set; }
        public State OrderCreatingState { get; private set; }
    }
}
