using MediatR;
using MultiShop.Order.Application.Features.Mediator.Dtos;
using MultiShop.Shared.Dtos;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Commands
{
    public sealed record CreateOrderingCommand(
          Guid CorrelationId,
         string UserId, 
         decimal TotalPrice, 
         DateTime OrderDate,
         List<OrderDetailDto> OrderItems, 
         AddressDto Address, 
         string OrderNumber
        
        
        ):IRequest;
   
}
