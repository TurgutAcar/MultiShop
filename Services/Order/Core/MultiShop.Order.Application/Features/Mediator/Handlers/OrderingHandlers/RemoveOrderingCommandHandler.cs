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
    internal sealed class RemoveOrderingCommandHandler(
            IRepository<Ordering> _repository,
            IUnitOfWork unitOfWork

        ) : IRequestHandler<RemoveOrderingCommand,Result<string>>
    {


        public async Task<Result<string>> Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
        {

            var value =await _repository.GetByIdAsync(request.Id);

            await _repository.DeleteAsync(value);
            await unitOfWork.SaveChangesAsync();
            return "Ordering silindi";

        }
    }
}
