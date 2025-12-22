using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands.StockCommands;
using MultiShop.Shared.Events.Dtos;

namespace MultiShop.Order.Infrastructure.Messaging
{
    public class StockReservedEventConsumer
    {
        private readonly IMediator _mediator;

        public StockReservedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task HandleAsync(StockReservedEvent @event)
        {
            await _mediator.Send(new StockReservedCommand(
                @event.SagaId,
                @event.OrderId,
                @event.ProductId,
                @event.ReservedQuantity
            ));
        }
    }

}
