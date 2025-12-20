using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockItemQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal sealed class GetStockReservationByIdQueryHandler(
         IMapper mapper,
         IStockReservationRepository stockReservationRepository) : IRequestHandler<GetStockReservationByIdQuery, Result<GetStockReservationByIdQueryResult>>
    {
        public async Task<Result<GetStockReservationByIdQueryResult>> Handle(GetStockReservationByIdQuery request, CancellationToken cancellationToken)
        {

            var value = await stockReservationRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            var map = mapper.Map<GetStockReservationByIdQueryResult>(value);
            return map;
        }
    }
}
