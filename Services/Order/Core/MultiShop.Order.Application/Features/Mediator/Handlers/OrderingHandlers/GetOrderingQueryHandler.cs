
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Domain.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    internal sealed class GetOrderingQueryHandler(
         IRepository<Ordering> _repository,
         IMapper _mapper
        ) : IRequestHandler<GetOrderingQuery,Result<List<GetOrderingQueryResult>>>
    {

        public async Task<Result<List<GetOrderingQueryResult>>> Handle(GetOrderingQuery request, CancellationToken cancellationToken)
        {
           // var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            var values=await  _repository.GetAllAsync();
            var mapList = values.Select(x => _mapper.Map<GetOrderingQueryResult>(x)).ToList();
            return mapList;
            //return values.Select(x=>new GetOrderingQueryResult{
            //    OrderingId=x.OrderingId,
            //    OrderDate=x.OrderDate,
            //    TotalPrice=x.TotalPrice,
            //    UserId=x.UserId,
            //}).ToList();
        }
    }
}
