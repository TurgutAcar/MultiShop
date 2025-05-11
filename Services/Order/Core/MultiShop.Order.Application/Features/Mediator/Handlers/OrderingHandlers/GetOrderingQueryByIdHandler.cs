using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class GetOrderingQueryByIdHandler : IRequestHandler<GetOrderingByIdQuery, GetOrderingByIdQueryResult>
    {
        private readonly IRepository<Ordering> _repository;

        public GetOrderingQueryByIdHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderingByIdQueryResult> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
        {
            var value= await _repository.GetByIdAsync(request.Id);
            return new GetOrderingByIdQueryResult
            {
                OrderDate=value.OrderDate,
                OrderingId=value.OrderingId,
                TotalPrice=value.TotalPrice,
                UserId=value.UserId,    
            };
        }
    }
}
