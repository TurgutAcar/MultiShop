using MediatR;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands
{
    public sealed record RemoveStockIReservationCommand(
       int Id) : IRequest<Result<string>>;
}
