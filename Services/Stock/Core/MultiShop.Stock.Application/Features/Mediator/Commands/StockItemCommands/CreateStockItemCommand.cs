using MediatR;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Shared.Responses;


namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record CreateStockItemCommand(
         string ProductId,int TotalQuantity,int ReservedQuantity, bool IsActive,
         int Type, int ReferenceId,string Description,
    DateTime UpdatedAt) : IRequest<Result<string>>;
   
}
