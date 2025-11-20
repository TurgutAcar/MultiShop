using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class GetOrderingQueryByIdHandler(
          IRepository<Ordering> _repository,
          IMapper mapper
        ) : IRequestHandler<GetOrderingByIdQuery,Result<GetOrderingByIdQueryResult>>
    {
     
        public async Task<Result<GetOrderingByIdQueryResult>> Handle(GetOrderingByIdQuery request, CancellationToken cancellationToken)
        {
            var value= await _repository.GetByIdAsync(request.Id);
            var map = mapper.Map<GetOrderingByIdQueryResult>(value);
            return map;
            //return new GetOrderingByIdQueryResult
            //{
            //    OrderDate=value.OrderDate,
            //    OrderingId=value.OrderingId,
            //    TotalPrice=value.TotalPrice,
            //    UserId=value.UserId,    
            //};
        }
    }
}
