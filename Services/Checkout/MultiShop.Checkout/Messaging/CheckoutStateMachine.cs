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
            // SelectId sayesinde gelen mesajdaki CorrelationId ile DB'deki Saga eşleşir.
            Event(() => CheckoutStartedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => StockReservedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => StockReservationFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));

            Event(() => PaymentCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => PaymentFailedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            Event(() => OrderCompletedEvent, x => x.CorrelateById(context => context.Message.CorrelationId));
            // 3. Akış Tanımı
            Initially(
                When(CheckoutStartedEvent)
                    .Then(context =>
                    {
                        // Gelen verileri Saga State'e (Veritabanına) kaydediyoruz
                        context.Saga.UserId = context.Message.UserId;
                        context.Saga.Items = context.Message.Items;
                        context.Saga.CreatedAt = DateTime.UtcNow;
                    })
                    .Publish(context => new StockReserveRequestedEvent
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        Items = context.Saga.Items
                    })
                    .TransitionTo(StockReservedState) // Durumu "Stok Bekleniyor"a çek
            );
            // STOK BEKLERKEN GELEN CEVAPLAR
            During(StockReservedState,
                // 1. BAŞARILI DURUM: Ödeme bekleniyor aşamasına geç
                When(StockReservedEvent)
                    .Then(context => Console.WriteLine($"Sipariş {context.Saga.CorrelationId} için stok rezerve edildi."))
                    .Publish(context => new PaymentServiceRequestedEvent
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        UserId = context.Saga.UserId,
                        CardNumber = context.Saga.CardNumber, // Bu bilgi CheckoutStarted ile gelmişti
                        TotalAmount = context.Saga.TotalAmount
                    })
                    .TransitionTo(PaymentPendingState),

                // 2. HATALI DURUM: Hata detaylarını kaydet ve iptal et
                When(StockReservationFailedEvent)
                    .Then(context =>
                    {
                        context.Saga.FailureReason = context.Message.Reason;
                        context.Saga.FailedItems = context.Message.FailedItems;
                    })
                    .TransitionTo(CancelledState)
                    );
                    During(PaymentPendingState,
                    When(PaymentCompletedEvent)
                        .Then(context => {
                            // ÖDEME OK! Şimdi Order Service'e "Siparişi Oluştur" emri veriyoruz.
                        })
                        .Publish(context => new OrderRequestEvent
                        {
                            CorrelationId = context.Saga.CorrelationId,
                            UserId = context.Saga.UserId,
                            TotalPrice = context.Saga.TotalAmount,
                            OrderDate=context.Saga.OrderDate,
                            OrderItems = context.Saga.Items,
                            Address=context.Saga.Address,
                            OrderNumber=context.Saga.OrderNumber
                        })
                        .TransitionTo(OrderCreatingState), // Yeni bir state: Sipariş oluşturuluyor

                    When(PaymentFailedEvent)
                        .Publish(context => new RollbackStockRequestedEvent { CorrelationId = context.Saga.CorrelationId })
                        .TransitionTo(CancelledState)
                );
                                During(OrderCreatingState,
                        When(OrderCompletedEvent)
                            .Then(context => Console.WriteLine($"TEBRİKLER! Sipariş {context.Saga.CorrelationId} başarıyla tamamlandı."))
                            .Finalize() // Saga kaydını sonlandırır (Redis/DB'den silinebilir hale getirir)
);

            // State Machine bittiğinde bu kayıt silinsin mi? (Opsiyonel)
            SetCompletedWhenFinalized();

            // Durum Tanımları
            // State Machine bu değişkenleri otomatik doldurur.
        }

        // Isim çakışmasını önlemek için "MassTransit.Event" tam adı kullanıldı
        public MassTransit.Event<ICheckoutStarted> CheckoutStartedEvent { get; private set; }
        public MassTransit.Event<IStockReservedEvent> StockReservedEvent { get; private set; }
        public MassTransit.Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; private set; }
        public MassTransit.Event<IOrderCompletedEvent> OrderCompletedEvent { get; private set; }

        public MassTransit.Event<IStockReservationFailedEvent> StockReservationFailedEvent { get; private set; }
        public MassTransit.Event<IPaymentFailedEvent> PaymentFailedEvent { get; private set; }

        public State StockReserved { get; private set; }

        // Property Tanımları
        public State StockReservedState { get; private set; }
        public State PaymentPendingState { get; private set; }
        public State CancelledState { get; private set; }
        public State OrderCreatingState { get; private set; }

    }
}
