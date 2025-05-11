using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class RemoveOrderingCommandHandler : IRequestHandler<RemoveOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;

        public RemoveOrderingCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
        {
            var value =await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(value);
        }
    }
}
