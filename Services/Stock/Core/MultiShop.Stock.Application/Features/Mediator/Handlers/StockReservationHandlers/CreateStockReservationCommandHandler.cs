using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Application.Messaging;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Events.Dtos;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Domain.Repositories;


namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockReservationHandlers
{
    internal sealed class CreateStockReservationCommandHandler(
        IStockReservationRepository stockReservationRepository,
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork,
        IEventBus eventBus) : IRequestHandler<CreateStockReservationCommand>
    {
        public async Task Handle(CreateStockReservationCommand request, CancellationToken cancellationToken)
        {
            var stock= await stockItemRepository.GetByExpressionAsync(p => p.ProductId == request.ProductId, cancellationToken);
            if(stock is null ||stock.AvailableQuantity < request.Quantity)
            {
                await eventBus.PublishAsync(new StockReservationFailedEvent
                {
                    SagaId = request.SagaId,
                    OrderId = request.OrderId,
                    ProductId = request.ProductId,
                    Reason = "Yetersiz stok"
                });
                return;
               
            }
            stock.TotalQuantity -= request.Quantity;
            stock.ReservedQuantity += request.Quantity;
            await unitOfWork.SaveChangesAsync();

            await eventBus.PublishAsync(new StockReservedEvent
            {
                SagaId = request.SagaId,
                OrderId = request.OrderId,
                ProductId = request.ProductId,
                ReservedQuantity = request.Quantity
            });
            // var mapValue = mapper.Map<StockReservation>(request);
            //  await stockReservationRepository.AddAsync(mapValue);
            //  await unitOfWork.SaveChangesAsync();
            //return "StockReservation olusturuldu";
        }
    }
}
