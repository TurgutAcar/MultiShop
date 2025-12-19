using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Shared.Responses;


namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries
{
    public sealed record  GetStockReservationQuery() 
        : IRequest<Result<GetStockReservationQueryResult>>;


}
