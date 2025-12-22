using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public sealed class UpdateOrderDetailCommandHandler(
          IRepository<OrderDetail> _repository,
          IMapper _mapper,
        IUnitOfWork unitOfWork
)
    {
    
        public async Task<Result<string>> Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var value = await _repository.GetByIdAsync(updateOrderDetailCommand.OrderDetailId);
            _mapper.Map(updateOrderDetailCommand, value);

            await _repository.UpdateAsync(value);
            await unitOfWork.SaveChangesAsync();
            return "OrderDetail kaydedildi";
            //value.ProductId = updateOrderDetailCommand.ProductId;
            //value.ProductName = updateOrderDetailCommand.ProductName;
            //value.ProductPrice = updateOrderDetailCommand.ProductPrice;
            //value.ProductAmount = updateOrderDetailCommand.ProductAmount;
            //value.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice;
            //value.OrderingId = updateOrderDetailCommand.OrderingId;
        }
    }
}
