using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands;
using MultiShop.Services.Stock.Core.Domain.Events;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.EventHandlers.StockTransactionsEventHandlers
{
    internal sealed class CreateStockTransactionEventHandler(
         IMapper mapper,
        IStockTransactionRepository stockTransactionRepository,
        IUnitOfWork unitOfWork) : INotificationHandler<StockTransactionCreatedEvent>
    {
        public async Task Handle(StockTransactionCreatedEvent notification, CancellationToken cancellationToken)
        {
            var mapValue = mapper.Map<StockTransaction>(notification);
            await stockTransactionRepository.AddAsync(mapValue);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
