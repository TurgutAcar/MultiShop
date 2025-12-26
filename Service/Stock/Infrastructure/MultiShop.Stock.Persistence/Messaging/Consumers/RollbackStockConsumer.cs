using MassTransit;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Shared.Events;
using MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Persistence.Messaging.Consumers
{
  
    public class RollbackStockConsumer(
       IMediator _mediator):IConsumer<RollbackStockRequestedEvent>
    {
        public async Task Consume(ConsumeContext<RollbackStockRequestedEvent> context)
        {
            await _mediator.Send
                 (new RollbackStockItemCommand(context.Message.CorrelationId, context.Message.Items));
        }

       
    }
}
