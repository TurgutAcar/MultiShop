using MediatR;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands
{
    public sealed record UpdateStockReservationCommand(
        int Id,
       string ProductId,
       int Quantity,
     string CartId,
     DateTime ExpiresAt,
     int StatusValue) : IRequest<Result<string>>;
}
