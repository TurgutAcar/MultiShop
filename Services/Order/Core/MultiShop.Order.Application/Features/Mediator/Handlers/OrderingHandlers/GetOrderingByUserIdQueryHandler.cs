
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class GetOrderingByUserIdQueryHandler(
         IOrderingRepository _orderingRepository,
         IMapper _mapper

        ) : IRequestHandler<GetOrderingByUserIdQuery,Result<List<GetOrderingByUserIdQueryResult>>>
    {
      

        public async Task<Result<List<GetOrderingByUserIdQueryResult>>>Handle(GetOrderingByUserIdQuery request, CancellationToken cancellationToken)
        {
            var values =_orderingRepository.GetOrderingsByUserId(request.Id);
            var mapList = values.Select(x => _mapper.Map<GetOrderingByUserIdQueryResult>(x)).ToList();
            return mapList;
            //return values.Select(x => new GetOrderingByUserIdQueryResult
            //{
            //    OrderDate = x.OrderDate,
            //    OrderingId = x.OrderingId,
            //    TotalPrice = x.TotalPrice,
            //    UserId = x.UserId,
            //}).ToList();
        }
    }
}
