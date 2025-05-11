using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailByIdQuery;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailQueryByIdHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public GetOrderDetailQueryByIdHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }
        public async Task<GetOrderResultByIdQueryResult> Handle(GetOrderDetailByIdQuery getOrderDetailByIdQuery)
        {
            var value = await _repository.GetByIdAsync(getOrderDetailByIdQuery.Id);
            return new GetOrderResultByIdQueryResult
            {
                OrderDetailId=value.OrderDetailId,
                OrderingId=value.OrderingId,
                ProductAmount=value.ProductAmount,
                ProductId=value.ProductId,
                ProductName=value.ProductName,
                ProductPrice=value.ProductPrice,
                ProductTotalPrice=value.ProductTotalPrice,
                
            };
        }
    }
}
