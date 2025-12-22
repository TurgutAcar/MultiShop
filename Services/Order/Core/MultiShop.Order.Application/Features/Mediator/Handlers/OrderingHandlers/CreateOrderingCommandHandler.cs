using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.OrderSagaAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Services.Order.Core.Application.Messaging;
using MultiShop.Shared.Events.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class CreateOrderingCommandHandler(
         IRepository<Ordering> _repository,
         IRepository<OrderSaga> _orderSagaRepository,

         IMapper _mapper,
         IUnitOfWork unitOfWork,
         IEventBus _eventBus

        ) : IRequestHandler<CreateOrderingCommand,Result<string>>
    {
      
        public async Task<Result<string>> Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            var map=_mapper.Map<Ordering>(request);     
            await _repository.CreateAsync(map);
            await unitOfWork.SaveChangesAsync();

            var saga =new OrderSaga(map.OrderingId);

            await _orderSagaRepository.CreateAsync(saga);

            await unitOfWork.SaveChangesAsync();

            foreach (var item in map.OrderDetails)
            {
                await _eventBus.PublishAsync(new StockReserveRequestedEvent
                {
                    SagaId = saga.SagaId,
                    OrderId = map.OrderingId,
                    ProductId = item.ProductId,
                    Quantity = item.ProductAmount,
                    RequestedAt = DateTime.UtcNow
                });
            }
            return "Ordering olusturuldu";
        }
    }
}
