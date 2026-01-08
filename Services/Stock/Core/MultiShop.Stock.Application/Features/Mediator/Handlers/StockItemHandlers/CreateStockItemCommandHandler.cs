using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.Events;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Core.Domain.ValueObjects.Enums;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Core.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class CreateStockItemCommandHandler(
        IMapper mapper,
        IMediator mediator,
        IStockItemRepository stockItemRepository,
       // IStockTransactionRepository stockTransactionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
        {
            var mapValue = mapper.Map<StockItem>(request);
            await stockItemRepository.AddAsync(mapValue);
            await mediator.Publish(
            new StockTransactionCreatedEvent(
                request.ProductId,
                request.TotalQuantity,
                request.Type,
                request.ReferenceId,
                DateTime.UtcNow,
                request.Description
            ),
            cancellationToken
        );
           // var transactionMapValue = mapper.Map<StockTransaction>(request);
            //StockTransaction stockTransaction = new StockTransaction
            //{
            //    ProductId = request.ProductId,
            //    Quantity = request.TotalQuantity,
            //    Type = StockTransactionType.FromValue(request.Type),
            //    CreatedAt = DateTime.UtcNow,
            //    ReferenceId=request.ReferenceId,
            //    Description= request.Description
               
            //};
            //await stockTransactionRepository.AddAsync(stockTransaction);
            await unitOfWork.SaveChangesAsync();
            return "Stock olusturuldu";
        }
    }
}
