using MediatR;
using MultiShop.Shared.Dtos;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands
{
    public sealed record CreateStockReservationCommand(
        Guid CorrelationId,
        List<OrderDetailDto> Items
     ):IRequest;

   
}
