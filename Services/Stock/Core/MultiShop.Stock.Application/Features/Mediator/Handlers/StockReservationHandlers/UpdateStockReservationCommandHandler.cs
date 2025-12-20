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
    internal sealed class UpdateStockReservationCommandHandler(
         IMapper mapper,
        IStockReservationRepository stockReservationRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateStockReservationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateStockReservationCommand request, CancellationToken cancellationToken)
        {
            StockReservation? stockReservation = await stockReservationRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (stockReservation is null)
            {
                return Result<string>.Failure("StockReservation bulunamadı!");

            }
            var mapValue = mapper.Map<StockReservation>(request);
            stockReservationRepository.Update(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "StockReservation kaydedildi";
        }
    }
}
