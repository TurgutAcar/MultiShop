using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.StockCommands;
using MultiShop.Shared.Events;

namespace MultiShop.Order.Infrastructure.Messaging.Consumer
{
    //public class StockReservationFailedEventConsumer
    //{
    //    private readonly IMediator _mediator;

    //    public StockReservationFailedEventConsumer(IMediator mediator)
    //    {
    //        _mediator = mediator;
    //    }

    //    public async Task HandleAsync(StockReservationFailedEvent @event)
    //    {
    //        await _mediator.Send(new StockReservationFailedCommand(
    //            @event.SagaId,
    //            @event.OrderId,
    //            @event.ProductId,
    //            @event.Reason
    //        ));
    //    }
    //}
}
