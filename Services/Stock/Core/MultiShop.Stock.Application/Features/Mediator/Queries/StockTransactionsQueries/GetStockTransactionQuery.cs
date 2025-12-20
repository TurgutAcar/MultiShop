using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockTransactionResult;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockTransactionsQueries
{
    public sealed record  GetStockTransactionQuery :
        IRequest<Result<List<GetStockTransactionQueryResult>>>;


}
