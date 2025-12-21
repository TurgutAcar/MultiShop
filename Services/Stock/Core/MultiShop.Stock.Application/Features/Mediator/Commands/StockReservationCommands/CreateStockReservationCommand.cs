using MediatR;
using MultiShop.Shared.Responses;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands
{
    public sealed record CreateStockReservationCommand(
       string ProductId,
       int Quantity,
     string CartId,
     DateTime ExpiresAt ,
     int StatusValue):IRequest<Result<string>>;
    
    
}
