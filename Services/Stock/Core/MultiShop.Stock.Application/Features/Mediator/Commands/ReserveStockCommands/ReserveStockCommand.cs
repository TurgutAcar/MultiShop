using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Commands.ReserveStockCommands
{
    public sealed record ReserveStockCommand(
    Guid SagaId,
    int OrderId,
    string ProductId,
    int Quantity
) : IRequest;

}
