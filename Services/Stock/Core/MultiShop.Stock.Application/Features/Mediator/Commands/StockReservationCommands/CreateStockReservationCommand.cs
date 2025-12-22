using MediatR;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands
{
    public sealed record CreateStockReservationCommand(
        int SagaId,
        int OrderId,
       string ProductId,
       int Quantity
     ):IRequest;
    
    
}
