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
    internal sealed class GetStockItemByIdQueryHandler(
        IMapper mapper,
        IStockItemRepository stockItemRepository) : IRequestHandler<GetStockItemByIdQuery, Result<GetStockItemByIdQueryResult>>
    {
        public async Task<Result<GetStockItemByIdQueryResult>> Handle(GetStockItemByIdQuery request, CancellationToken cancellationToken)
        {
            
            var value = await stockItemRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            var map = mapper.Map<GetStockItemByIdQueryResult>(value);
            return map;
        }
    }
}
