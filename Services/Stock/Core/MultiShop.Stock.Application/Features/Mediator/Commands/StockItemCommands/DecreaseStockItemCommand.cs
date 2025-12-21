
using MediatR;
using MultiShop.Shared.Responses;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record DecreaseStockItemCommand(
     string ProductId,
     int Quantity ,
     int Type ,
     string Description ) : IRequest<Result<string>>;


}
