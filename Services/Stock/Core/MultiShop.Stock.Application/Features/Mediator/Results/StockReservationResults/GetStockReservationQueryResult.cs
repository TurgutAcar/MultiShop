using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;


namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults
{
    public sealed record GetStockReservationQueryResult(
           int Id,
     string ProductId,
     int Quantity,
     string CartId,
     DateTime ExpiresAt,
     ReservationStatus Status);
}
