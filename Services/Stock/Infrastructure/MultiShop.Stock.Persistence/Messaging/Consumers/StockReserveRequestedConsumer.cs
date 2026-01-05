using MassTransit;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Persistence.Messaging.Consumers
{
    public class StockReserveRequestedConsumer(
        IMediator _mediator):IConsumer<StockReserveRequestedEvent>
    {
        public async Task Consume(ConsumeContext<StockReserveRequestedEvent> context)
        {
            await _mediator.Send
                (new CreateStockReservationCommand(context.Message.CorrelationId, context.Message.Items));
        }

       
    }

}
