using AutoMapper;
using MassTransit;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Services.Order.Core.Application.Messaging;
using MultiShop.Shared.Events;
using MultiShop.Shared.Events.EventInterface;
using System.Net;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class CreateOrderingCommandHandler(
         IRepository<Ordering> _repository,
         IMapper _mapper,
         IUnitOfWork unitOfWork,
         IPublishEndpoint _publishEndpoint

        ) : IRequestHandler<CreateOrderingCommand>
    {
      
        public async Task Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            
           // var map=_mapper.Map<Ordering>(request);
            var newAddress = new Address(request.Address.AddressId, request.Address.UserId, request.Address.Name, request.Address.Surname, request.Address.Email, request.Address.Phone, request.Address.Country, request.Address.District, request.Address.City, request.Address.Detail1, request.Address.Detail2,request.Address.Description,request.Address.ZipCode);
            Ordering newOrder = new Ordering(request.UserId, request.TotalPrice, request.OrderDate, newAddress);
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderDetail(x.ProductId, x.ProductName,x.ProductPrice,x.ProductAmount,x.ProductTotalPrice);
            });
            await _repository.CreateAsync(newOrder);
            await unitOfWork.SaveChangesAsync();

           // var saga =new OrderSaga(map.OrderingId);

            //await _orderSagaRepository.CreateAsync(saga);

           // await unitOfWork.SaveChangesAsync();

            var orderCompletedEvent = new OrderCompletedEvent
            {
                CorrelationId = request.CorrelationId,

            };
            await _publishEndpoint.Publish<IOrderCompletedEvent>(orderCompletedEvent);



        }
    }
}
