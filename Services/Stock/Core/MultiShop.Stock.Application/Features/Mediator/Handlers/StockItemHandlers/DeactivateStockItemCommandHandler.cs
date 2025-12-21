using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
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
    internal sealed class DeactivateStockItemCommandHandler(
         IMapper mapper,
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeactivateStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeactivateStockItemCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem=await stockItemRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(stockItem is null)
            {
                return Result<string>.Failure("Stock bulunamadı!");
            }
            if (!stockItem.IsActive)
                return Result<string>.Failure("StockItem zaten aktif değil!");
            stockItem.IsActive = false;
            stockItem.UpdatedAt = DateTime.UtcNow;
            stockItemRepository.Update(stockItem);
            await unitOfWork.SaveChangesAsync();
            return "Stock kaydedildi";
        }
    }
}
