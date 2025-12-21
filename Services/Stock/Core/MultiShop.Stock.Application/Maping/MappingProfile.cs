using AutoMapper;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockReservationCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockReservationQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Queries.StockTransactionsQueries;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockItemResults;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockReservationResults;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Results.StockTransactionResult;
using MultiShop.Services.Stock.Core.Domain.Events;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;

namespace MultiShop.Services.Stock.Core.Application.Maping
{
    public sealed class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<StockItem, GetStockItemQueryResult>();
            CreateMap<StockItem, GetStockItemByIdQueryResult>();
            CreateMap<StockReservation, GetStockReservationQueryResult>();
            CreateMap<StockReservation, GetStockReservationByIdQueryResult>();
            CreateMap<StockTransaction, GetStockTransactionQueryResult>();
            CreateMap<StockTransaction, GetStockTransactionByIdQueryResult>();

            CreateMap<UpdateStockItemCommand, StockItem>();
            CreateMap<CreateStockItemCommand, StockItem>();
            CreateMap<DeactivateStockItemCommand, StockItem>();

            CreateMap<CreateStockReservationCommand, StockReservation>()
                .ForMember(member => member.Status,
                options => options.MapFrom(s => ReservationStatus.FromValue(s.StatusValue)));
           
                CreateMap<UpdateStockReservationCommand, StockReservation>()
               .ForMember(member => member.Status,
               options => options.MapFrom(s => ReservationStatus.FromValue(s.StatusValue)));
           
            CreateMap<StockTransactionCreatedEvent, StockTransaction>()
              .ForMember(member => member.Type,
              options => options.MapFrom(s => StockTransactionType.FromValue(s.TransactionTypeValue)));
            
          
            //CreateMap<IncreaseStockItemCommand, StockTransaction>()
            //   .ForMember(member => member.Type,
            //   options => options.MapFrom(s => StockTransactionType.FromValue(s.Type)));

            //CreateMap<DecreaseStockItemCommand, StockTransaction>()
            //   .ForMember(member => member.Type,
            //   options => options.MapFrom(s => StockTransactionType.FromValue(s.Type)));
        }
    }
}
