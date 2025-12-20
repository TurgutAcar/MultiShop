using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal class RemoveStockReservationCommandHandler(
        IStockReservationRepository stockReservationRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RemoveStockReservationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RemoveStockReservationCommand request, CancellationToken cancellationToken)
        {
           StockReservation? stockReservation=await stockReservationRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(stockReservation is null)
            {
                return "StockReservation bulunamadı.";
            }
             stockReservationRepository.Delete(stockReservation);
            await unitOfWork.SaveChangesAsync();
            return "StockReservation silindi.";

        }
    }
}
