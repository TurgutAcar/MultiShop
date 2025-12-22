using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public sealed class RemoveOrderDetailCommandHandler(
             IRepository<OrderDetail> _repository,
             IUnitOfWork unitOfWork
        )
    {

      
        public async Task<Result<string>> Handle(RemoveOrderDetailCommand removeOrderDetailCommand) 
        {
            var value = await _repository.GetByIdAsync(removeOrderDetailCommand.Id);
            await _repository.DeleteAsync(value);
            await unitOfWork.SaveChangesAsync();
            return "OrderDetail silindi";

        }
    }
}
