using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingCommandHandler(
          IRepository<Ordering> _repository,
         IMapper _mapper,
         IUnitOfWork unitOfWork

        ) : IRequestHandler<UpdateOrderingCommand,Result<string>>
    {
       
        public async Task<Result<string>> Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
        {
           var value=await _repository.GetByIdAsync(request.OrderingId);

            _mapper.Map(request, value); 

            await _repository.UpdateAsync(value);
            await unitOfWork.SaveChangesAsync();
            return "Ordering kaydedildi.";

            //value.OrderDate = request.OrderDate;
            //value.UserId = request.UserId;
            //value.TotalPrice = request.TotalPrice;
        }
    }
}
