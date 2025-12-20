using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal sealed class CreateStockReservationCommandHandler(
         IMapper mapper,
        IStockReservationRepository stockReservationRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStockReservationCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateStockReservationCommand request, CancellationToken cancellationToken)
        {
            var mapValue = mapper.Map<StockReservation>(request);
            await stockReservationRepository.AddAsync(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "StockReservation olusturuldu";
        }
    }
}
