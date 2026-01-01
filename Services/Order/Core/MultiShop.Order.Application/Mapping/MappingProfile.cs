using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Features.Mediator.Commands;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Shared.Dtos;


namespace MultiShop.Order.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<AddressDto, Address>();
            //CreateMap<OrderDetailDto, OrderDetail>();

            //// Command → Ordering mapping (factory üzerinden)
            //CreateMap<CreateOrderingCommand, Ordering>()
            //    .ConvertUsing(src => OrderingFactory.CreateFromCommand(src));


            CreateMap<CreateAddressCommand, Address>();
            CreateMap<RemoveAddressCommand, Address>();
            CreateMap<UpdateAddressCommand, Address>();

            CreateMap<CreateOrderingCommand, Ordering>();
            CreateMap<RemoveOrderingCommand, Ordering>();
            CreateMap<UpdateOrderingCommand, Ordering>();

            CreateMap<CreateOrderDetailCommand, OrderDetail>();
            CreateMap<RemoveOrderDetailCommand, OrderDetail>();
            CreateMap<UpdateOrderDetailCommand, Ordering>();

            CreateMap<Ordering, GetOrderingByUserIdQueryResult>()
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
           .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice))
           .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
           .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));



        }
    }
}
