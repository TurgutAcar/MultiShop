using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.StockCommands
{
    public sealed record StockReservedCommand(
    int SagaId,
    int OrderId,
    string ProductId,
    int Quantity
) : IRequest;

}
