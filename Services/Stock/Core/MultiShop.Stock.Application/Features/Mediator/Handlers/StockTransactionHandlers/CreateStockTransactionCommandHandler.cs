using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
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

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockTransactionHandlers
{
    internal sealed class CreateStockTransactionCommandHandler(
         IMapper mapper,
        IStockTransactionRepository stockTransactionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStockTransactionCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateStockTransactionCommand request, CancellationToken cancellationToken)
        {
            var mapValue = mapper.Map<StockTransaction>(request);
            await stockTransactionRepository.AddAsync(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "StockTransaction olusturuldu";
        }
    }
}
