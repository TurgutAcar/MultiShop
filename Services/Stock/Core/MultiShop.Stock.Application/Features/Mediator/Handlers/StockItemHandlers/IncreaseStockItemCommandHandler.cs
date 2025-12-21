using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;


namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class IncreaseStockItemCommandHandler(
        IStockItemRepository stockItemRepository,
        IStockTransactionRepository stockTransactionRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<IncreaseStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(IncreaseStockItemCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem = await stockItemRepository.GetByExpressionAsync(p => p.ProductId == request.ProductId, cancellationToken);
            if (stockItem is null)
            {
                return Result<string>.Failure("Stock bulunamadı!");

            }
            stockItem.TotalQuantity += request.Quantity;
            stockItem.UpdatedAt = DateTime.UtcNow;

            stockItemRepository.Update(stockItem);
            var mapValue = mapper.Map<StockTransaction>(request);
            await stockTransactionRepository.AddAsync(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "Stock kaydedildi";
        }
    }
}
