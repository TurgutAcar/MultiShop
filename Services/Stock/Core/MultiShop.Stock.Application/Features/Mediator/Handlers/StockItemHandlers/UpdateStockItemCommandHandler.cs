using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Core.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class UpdateStockItemCommandHandler(
        IMapper mapper,
        IStockTransactionRepository stockTransactionRepository,
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateStockItemCommand request, CancellationToken cancellationToken)
        {
           StockItem ? stockItem = await stockItemRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);
           if(stockItem is null)
            {
                return Result<string>.Failure("Stock bulunamadı!");

            }
             stockItem.UpdatedAt = DateTime.UtcNow;
            var mapValue = mapper.Map<StockItem>(request);
            stockItemRepository.Update(mapValue);
            StockTransaction stockTransaction = new StockTransaction
            {
                ProductId = request.ProductId,
                Quantity = request.TotalQuantity,
                Type = StockTransactionType.FromValue(request.Type),
                CreatedAt = DateTime.UtcNow,
                ReferenceId = request.ReferenceId,
                Description = request.Description

            };
            await stockTransactionRepository.AddAsync(stockTransaction);
            await unitOfWork.SaveChangesAsync();
            return "Stock kaydedildi";

        }
    }
}
