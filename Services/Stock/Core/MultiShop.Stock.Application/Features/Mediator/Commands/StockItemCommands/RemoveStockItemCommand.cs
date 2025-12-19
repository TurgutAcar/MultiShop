using MultiShop.Shared.Responses;
using MediatR;
namespace MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record RemoveStockItemCommand(

        int Id) : IRequest<Result<string>>;
}
