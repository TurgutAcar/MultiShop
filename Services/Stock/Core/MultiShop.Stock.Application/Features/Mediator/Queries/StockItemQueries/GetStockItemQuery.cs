using MediatR;
using MultiShop.Shared.Responses;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockItemQueries
{
    public sealed record GetStockItemQuery():IRequest<Result<GetStockItemQueryResult>>;
    
    
}
