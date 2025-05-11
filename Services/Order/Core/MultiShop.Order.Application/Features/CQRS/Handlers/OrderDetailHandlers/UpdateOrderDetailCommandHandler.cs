using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var value = await _repository.GetByIdAsync(updateOrderDetailCommand.OrderDetailId);
            value.ProductId = updateOrderDetailCommand.ProductId;
            value.ProductName = updateOrderDetailCommand.ProductName;
            value.ProductPrice = updateOrderDetailCommand.ProductPrice;
            value.ProductAmount = updateOrderDetailCommand.ProductAmount;
            value.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice;
            value.OrderingId = updateOrderDetailCommand.OrderingId;
            await _repository.UpdateAsync(value);
        }
    }
}
