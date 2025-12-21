using MediatR;
using MultiShop.Shared.Responses;


namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record CreateStockItemCommand(
         string ProductId,int TotalQuantity,int ReservedQuantity, bool IsActive,
    DateTime UpdatedAt) : IRequest<Result<string>>;
   
}
