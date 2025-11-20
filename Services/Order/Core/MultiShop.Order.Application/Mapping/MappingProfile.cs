using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Domain.OrderAggregate;


namespace MultiShop.Order.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAddressCommand, Address>();
            CreateMap<RemoveAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();

            CreateMap<CreateOrderingCommand, Ordering>();
            CreateMap<RemoveOrderingCommand, Ordering>();
            CreateMap<UpdateOrderingCommand, Ordering>();

            CreateMap<CreateOrderDetailCommand, OrderDetail>();
            CreateMap<RemoveOrderDetailCommand, OrderDetail>();
            CreateMap<UpdateOrderDetailCommand, Ordering>();

        }
    }
}
