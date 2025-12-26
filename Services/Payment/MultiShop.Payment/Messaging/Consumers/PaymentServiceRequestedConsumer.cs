using MassTransit;
using MultiShop.Shared.Events;

namespace MultiShop.Services.Messaging

{
    public class PaymentServiceRequestedConsumer(
        IPublishEndpoint _publishEndpoint
       ):IConsumer<PaymentServiceRequestedEvent>
    {
        public async Task Consume(ConsumeContext<PaymentServiceRequestedEvent> context)
        {
            bool isPaymentSuccessful = context.Message.TotalAmount < 10000; // Örn: 10.000 TL üzeri limit yetersiz olsun

            if (isPaymentSuccessful)
            {
                var paymentCompletedEvent = new PaymentCompletedEvent
                {
                    CorrelationId = context.Message.CorrelationId,

                };
                await _publishEndpoint.Publish<IPaymentCompletedEvent>(paymentCompletedEvent);


            }
            else
            {
                var paymentEventFailedEvent = new PaymentEventFailedEvent
                {
                    CorrelationId = context.Message.CorrelationId,
                    Reason = "Yetersiz bakiye veya limit."
                };
                await _publishEndpoint.Publish<IPaymentFailedEvent>(paymentEventFailedEvent);


            }
        }

       
    }

}
