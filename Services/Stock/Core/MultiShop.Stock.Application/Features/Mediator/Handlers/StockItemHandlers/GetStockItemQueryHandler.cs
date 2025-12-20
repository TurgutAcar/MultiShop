using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockItemQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class GetStockItemQueryHandler(
        IMapper mapper,
        IStockItemRepository stockItemRepository) : IRequestHandler<GetStockItemQuery, Result<List<GetStockItemQueryResult>>>
    {
        public async Task<Result<List<GetStockItemQueryResult>>> Handle(GetStockItemQuery request, CancellationToken cancellationToken)
        {
            var values = stockItemRepository.GetAll();
            var mapList = values.Select(x => mapper.Map<GetStockItemQueryResult>(x)).ToList();
            return mapList;
        }
    }
}
