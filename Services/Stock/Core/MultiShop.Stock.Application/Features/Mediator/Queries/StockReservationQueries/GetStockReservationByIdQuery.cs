using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Shared.Responses;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries
{
    public sealed record GetStockReservationByIdQuery(
        int Id) : IRequest<Result<GetStockReservationByIdQueryResult>>;


}
