using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.Events;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands;


namespace MultiShop.Stock.Core.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class DecreaseStockItemCommandHandler(
        IStockItemRepository stockItemRepository,
        IMediator mediator,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<DecreaseStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DecreaseStockItemCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem = await stockItemRepository.GetByExpressionAsync(p => p.ProductId == request.ProductId, cancellationToken);
            if (stockItem is null)
            {
                return Result<string>.Failure("Stock bulunamadı!");

            }
            stockItem.TotalQuantity -= request.Quantity;
            stockItem.UpdatedAt = DateTime.UtcNow;

            stockItemRepository.Update(stockItem);
            await mediator.Publish(
           new StockTransactionCreatedEvent(
               request.ProductId,
               request.Quantity,
               request.Type,
               0,
               DateTime.UtcNow,
               request.Description
           ),
           cancellationToken
       );


            await unitOfWork.SaveChangesAsync();
            return "Stock kaydedildi";
        }
    }
}
