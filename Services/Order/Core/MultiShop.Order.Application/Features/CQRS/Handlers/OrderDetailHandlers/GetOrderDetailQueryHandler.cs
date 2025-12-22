
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public sealed class GetOrderDetailQueryHandler(
             IRepository<OrderDetail> _repository,
             IMapper _mapper

        )
    {
    
        public async Task<Result<List<GetOrderDetailQueryResult>>> Handle()
        {
            var values = await _repository.GetAllAsync();
            var mapList = values.Select(x => _mapper.Map<GetOrderDetailQueryResult>(x)).ToList();
            return mapList;
            //return   values.Select(x => new GetOrderDetailQueryResult
            //{
            //    OrderDetailId = x.OrderDetailId,
            //    OrderingId = x.OrderingId,
            //    ProductAmount = x.ProductAmount,
            //    ProductId = x.ProductId,
            //    ProductName = x.ProductName,
            //    ProductPrice = x.ProductPrice,
            //    ProductTotalPrice = x.ProductTotalPrice,
                
            //}).ToList();
            
        }
    }
}
