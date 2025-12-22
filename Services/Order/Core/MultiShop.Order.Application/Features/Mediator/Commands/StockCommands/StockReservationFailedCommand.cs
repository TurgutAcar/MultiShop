using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.Mediator.Commands.StockCommands
{
    public sealed record StockReservationFailedCommand(
    int SagaId,
    int OrderId,
    string ProductId,
    string Reason
) : IRequest;

}
