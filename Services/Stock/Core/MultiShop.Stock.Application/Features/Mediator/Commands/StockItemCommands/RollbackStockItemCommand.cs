
using MediatR;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record RollbackStockItemCommand(
     Guid CorrelationId,
     List<OrderDetailDto> Items
 ) : IRequest;


}
