
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailByIdQuery;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public sealed class GetOrderDetailQueryByIdHandler(
         IRepository<OrderDetail> _repository,
         IMapper _mapper
        )
    {
     
        public async Task<Result<GetOrderResultByIdQueryResult>> Handle(GetOrderDetailByIdQuery getOrderDetailByIdQuery)
        {
            var value = await _repository.GetByIdAsync(getOrderDetailByIdQuery.Id);
            var map=_mapper.Map<GetOrderResultByIdQueryResult>(value);
            return map;
           
        }
    }
}
