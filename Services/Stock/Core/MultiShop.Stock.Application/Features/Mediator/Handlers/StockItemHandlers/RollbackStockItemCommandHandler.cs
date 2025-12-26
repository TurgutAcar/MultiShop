using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Core.Domain.Events;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
using MultiShop.Services.Stock.Domain.Entities;
using MultiShop.Services.Stock.Domain.Repositories;
using MultiShop.Shared.Responses;
using MultiShop.Stock.Application.Features.Mediator.Commands.StockItemCommands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Stock.Application.Features.Mediator.Handlers.StockItemHandlers
{
    internal sealed class RollbackStockItemCommandHandler(
        IStockItemRepository stockItemRepository,
        IMediator mediator,
        IMapper mapper,
        IUnitOfWork unitOfWork) : IRequestHandler<RollbackStockItemCommand>
    {
        public async Task Handle(RollbackStockItemCommand request, CancellationToken cancellationToken)
        {
            foreach (var item  in request.Items)
            {
                var stock = await stockItemRepository.GetByExpressionAsync(x => x.ProductId == item.ProductId);
                if (stock != null)
                {
                    // REZERVASYONU GERİ ÇEK:
                    // ReservedQuantity'yi azaltıyoruz, böylece "AvailableQuantity" tekrar artmış oluyor.
                    stock.ReservedQuantity -= item.ProductAmount;
                }
            }
            await unitOfWork.SaveChangesAsync();



           
        }
    }
}
