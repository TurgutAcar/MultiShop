using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Shared.Events.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Persistence.Messaging.Consumers
{
    public class StockReserveRequestedConsumer(
        IMediator _mediator)
    {
        

        public async Task HandleAsync(StockReserveRequestedEvent @event)
        {
            await _mediator.Send
                (new CreateStockReservationCommand(@event.SagaId, @event.OrderId, @event.ProductId, @event.Quantity));
        }
    }

}
