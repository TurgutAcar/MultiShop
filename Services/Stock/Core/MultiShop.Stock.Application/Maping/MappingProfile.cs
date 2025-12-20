using AutoMapper;
using MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Maping
{
    public sealed class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateStockItemCommand, StockItem>();
            CreateMap<CreateStockItemCommand, StockItem>();
            CreateMap<RemoveStockItemCommand, StockItem>();

            CreateMap<CreateStockReservationCommand, StockReservation>()
                .ForMember(member => member.Status,
                options => options.MapFrom(s => ReservationStatus.FromValue(s.StatusValue)));
            CreateMap<UpdateStockReservationCommand, StockReservation>()
               .ForMember(member => member.Status,
               options => options.MapFrom(s => ReservationStatus.FromValue(s.StatusValue)));
            CreateMap<CreateStockTransactionCommand, StockReservation>()
              .ForMember(member => member.Status,
              options => options.MapFrom(s => ReservationStatus.FromValue(s.TransactionTypeValue)));
            CreateMap<UpdateStockTransactionCommand, StockReservation>()
               .ForMember(member => member.Status,
               options => options.MapFrom(s => ReservationStatus.FromValue(s.TransactionTypeValue)));
        }
    }
}
