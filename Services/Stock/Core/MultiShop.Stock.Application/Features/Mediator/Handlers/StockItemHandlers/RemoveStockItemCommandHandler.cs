using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands;
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
    internal sealed class RemoveStockItemCommandHandler(
         IMapper mapper,
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<RemoveStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RemoveStockItemCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem=await stockItemRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(stockItem is null)
            {
                return Result<string>.Failure("Stock bulunamadı!");
            }
            stockItemRepository.Delete(stockItem);
            await unitOfWork.SaveChangesAsync();
            return "Stock silindi";
        }
    }
}
