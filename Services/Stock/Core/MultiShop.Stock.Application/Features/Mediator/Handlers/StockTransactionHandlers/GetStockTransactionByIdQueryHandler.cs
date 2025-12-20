using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockTransactionsQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockTransactionResult;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockTransactionHandlers
{
    internal sealed class GetStockTransactionByIdQueryHandler(
         IMapper mapper,
         IStockTransactionRepository stockTransactionRepository) : IRequestHandler<GetStockTransactionByIdQuery, Result<GetStockTransactionByIdQueryResult>>
    {
        public async Task<Result<GetStockTransactionByIdQueryResult>> Handle(GetStockTransactionByIdQuery request, CancellationToken cancellationToken)
        {

            var value = await stockTransactionRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            var map = mapper.Map<GetStockTransactionByIdQueryResult>(value);
            return map;
        }
    }
}
