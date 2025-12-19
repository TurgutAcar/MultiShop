using MediatR;
using MultiShop.Shared.Responses;


namespace MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record CreateStockItemCommand(
         string ProductId,int TotalQuantity,int ReservedQuantity,
         DateTime UpdatedAt) : IRequest<Result<string>>;
   
}
