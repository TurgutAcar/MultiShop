using MultiShop.Shared.Responses;
using MediatR;
namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record DeactivateStockItemCommand(

        int Id) : IRequest<Result<string>>;
}
