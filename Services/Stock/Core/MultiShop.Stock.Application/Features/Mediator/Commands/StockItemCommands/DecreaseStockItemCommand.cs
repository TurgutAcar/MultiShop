using MediatR;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands
{
    public sealed record DecreaseStockItemCommand(
     string ProductId,
     int Quantity,
     int Type,
     string Description) : IRequest<Result<string>>;
}
