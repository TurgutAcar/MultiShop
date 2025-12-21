
using MediatR;
using MultiShop.Shared.Responses;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record IncreaseStockItemCommand(
     string ProductId,
     int Quantity,
     int Type ,
     string? ReferenceNo,
     string? Description) : IRequest<Result<string>>;


}
