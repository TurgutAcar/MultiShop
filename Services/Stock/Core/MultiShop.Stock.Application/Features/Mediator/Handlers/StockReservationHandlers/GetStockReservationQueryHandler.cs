using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal sealed class GetStockReservationQueryHandler(
        IStockReservationRepository stockReservationRepository,
        IMapper mapper) : IRequestHandler<GetStockReservationQuery, Result<List<GetStockReservationQueryResult>>>
    {
        public async Task<Result<List<GetStockReservationQueryResult>>> Handle(GetStockReservationQuery request, CancellationToken cancellationToken)
        {
            var values = stockReservationRepository.GetAll();
            var mapList = values.Select(x => mapper.Map<GetStockReservationQueryResult>(x)).ToList();

            return mapList;
        }
    }
}
