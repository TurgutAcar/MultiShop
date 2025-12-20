using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockTransactionHandlers
{
    internal sealed class UpdateStockTransactionCommandHandler(
        IMapper mapper,
        IStockTransactionRepository stockTransactionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateStockTransactionCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateStockTransactionCommand request, CancellationToken cancellationToken)
        {
            StockTransaction? stockTransaction = await stockTransactionRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
            if (stockTransaction is null)
            {
                return Result<string>.Failure("stockTransaction bulunamadı!");

            }
            var mapValue = mapper.Map<StockTransaction>(request);
            stockTransactionRepository.Update(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "stockTransaction kaydedildi";
        }
    }
}
