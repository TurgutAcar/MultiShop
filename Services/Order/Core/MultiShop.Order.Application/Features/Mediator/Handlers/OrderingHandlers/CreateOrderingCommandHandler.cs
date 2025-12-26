using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.OrderSagaAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Services.Order.Core.Application.Messaging;
using MultiShop.Shared.Events;
using MultiShop.Shared.Events.EventInterface;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class CreateOrderingCommandHandler(
         IRepository<Ordering> _repository,
         IRepository<OrderSaga> _orderSagaRepository,

         IMapper _mapper,
         IUnitOfWork unitOfWork,
         IEventBus _eventBus

        ) : IRequestHandler<CreateOrderingCommand>
    {
      
        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            
            var map=_mapper.Map<Ordering>(request);     
            await _repository.CreateAsync(map);
            await unitOfWork.SaveChangesAsync();

            var saga =new OrderSaga(map.OrderingId);

            await _orderSagaRepository.CreateAsync(saga);

            await unitOfWork.SaveChangesAsync();

            var orderCompletedEvent = new OrderCompletedEvent
            {
                CorrelationId = request.CorrelationId,

            };
            await _eventBus.PublishAsync<IOrderCompletedEvent>(orderCompletedEvent);


          
        }
    }
}
