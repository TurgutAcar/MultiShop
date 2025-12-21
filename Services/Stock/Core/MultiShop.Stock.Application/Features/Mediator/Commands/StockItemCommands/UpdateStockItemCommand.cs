using MediatR;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record UpdateStockItemCommand(

        int Id, string ProductId, int TotalQuantity, int ReservedQuantity, bool IsActive,
         DateTime UpdatedAt, int Type, int ReferenceId, string Description) : IRequest<Result<string>>;
}
