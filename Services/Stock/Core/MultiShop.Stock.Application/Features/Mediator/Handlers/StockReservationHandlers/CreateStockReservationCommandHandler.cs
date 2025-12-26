using MassTransit;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Events;
using MultiShop.Stock.Domain.Repositories;


namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal sealed class CreateStockReservationCommandHandler(
        IStockReservationRepository stockReservationRepository,
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint _publishEndpoint) : IRequestHandler<CreateStockReservationCommand>
    {
        public async Task Handle(CreateStockReservationCommand request, CancellationToken cancellationToken)
        {
            var failedItems = new List<FailedStockItem>();
            var stockItems = request.Items;
            foreach (var item in stockItems)
            {
                var productStock = await stockItemRepository.GetByExpressionAsync(p => p.ProductId == item.ProductId, cancellationToken);
                var availableStock = productStock != null ? productStock.AvailableQuantity : 0;
                if(availableStock < item.ProductAmount)
                {
                    failedItems.Add(new FailedStockItem
                    {
                        ProductId = item.ProductId,
                        Message = $"Yetersiz stok. Mevcut: {availableStock}, Talep: {item.ProductAmount}"
                    });
                }
            }
            if(!failedItems.Any())
            {
                foreach (var item in stockItems)
                {
                    var productStock = await stockItemRepository.GetByExpressionAsync(p => p.ProductId == item.ProductId, cancellationToken);
                    //productStock.TotalQuantity -= item.ProductAmount;
                    productStock.ReservedQuantity += item.ProductAmount;
                    
                }
                await unitOfWork.SaveChangesAsync();
                var stockReservedEvent = new StockReservedEvent
                {
                    CorrelationId = request.CorrelationId,

                };
                 await _publishEndpoint.Publish<IStockReservedEvent>(stockReservedEvent);


            }
            else
            {
                var stockReservationFailedEvent = new StockReservationFailedEvent
                {
                    CorrelationId = request.CorrelationId,
                    Reason = "Bazı ürünlerin stoğu yetersiz.",
                    FailedItems = failedItems
                };
                await _publishEndpoint.Publish<IStockReservationFailedEvent>(stockReservationFailedEvent);
            }
            
        }
    }
}
