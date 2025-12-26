using MassTransit;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Application.Features.Mediator.Commands.StockCommands;
using MultiShop.Order.Application.Features.Mediator.Dtos;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Events;

namespace MultiShop.Order.Infrastructure.Messaging
{
    public class OrderRequestedEventConsumer:IConsumer<OrderRequestEvent>
    {
        private readonly IMediator _mediator;

        public OrderRequestedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<OrderRequestEvent> context)
        {
            await _mediator.Send(new CreateOrderingCommand(
                context.Message.CorrelationId,
                context.Message.UserId,
                context.Message.TotalPrice,
                context.Message.OrderDate,
                context.Message.OrderItems,
                context.Message.Address,
                context.Message.OrderNumber
            ));
        }
    }

}
