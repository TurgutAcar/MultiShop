using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class CreateOrderingCommandHandler(
         IRepository<Ordering> _repository,
         IMapper _mapper,
         IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateOrderingCommand,Result<string>>
    {
      
        public async Task<Result<string>> Handle(CreateOrderingCommand request, CancellationToken cancellationToken)
        {
            var map=_mapper.Map<Ordering>(request);     
            await _repository.CreateAsync(map);
            await unitOfWork.SaveChangesAsync();
            return "Ordering olusturuldu";
        }
    }
}
