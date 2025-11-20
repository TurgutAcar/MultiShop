using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public sealed class CreateOrderDetailCommandHandler(
           IRepository<OrderDetail> _repository,
           IMapper _mapper,
           IUnitOfWork unitOfWork

        )
    {
      
        public async Task<Result<string>> Handle(CreateOrderDetailCommand createOrderDetailCommand)
        {
            var mapper = _mapper.Map<OrderDetail>(createOrderDetailCommand);

            await _repository.CreateAsync(mapper);
            await unitOfWork.SaveChangesAsync();
            return "OrderDetail oluşturuldu";



           // await _repository.CreateAsync(new OrderDetail
           // {
           //     ProductId = createOrderDetailCommand.ProductId,
           //     ProductName = createOrderDetailCommand.ProductName,
           //     ProductPrice = createOrderDetailCommand.ProductPrice,
           //     ProductAmount = createOrderDetailCommand.ProductAmount,
           //     ProductTotalPrice = createOrderDetailCommand.ProductTotalPrice,
           //     OrderingId = createOrderDetailCommand.OrderingId,
           // }
           //);
        }

    }
}
