using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockTransactionsQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockTransactionResult;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockTransactionHandlers
{
    internal sealed class GetStockTransactionQueryHandler(
         IStockTransactionRepository stockTransactionRepository,
         IMapper mapper) : IRequestHandler<GetStockTransactionQuery, Result<List<GetStockTransactionQueryResult>>>
    {
        public async Task<Result<List<GetStockTransactionQueryResult>>> Handle(GetStockTransactionQuery request, CancellationToken cancellationToken)
        {
            var values = stockTransactionRepository.GetAll();
            var mapList = values.Select(x => mapper.Map<GetStockTransactionQueryResult>(x)).ToList();

            return mapList;
        }
    }
}
