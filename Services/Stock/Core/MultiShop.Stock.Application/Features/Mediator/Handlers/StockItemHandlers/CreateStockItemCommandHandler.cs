using AutoMapper;
using MediatR;
using MultiShop.Services.Stock.Application.Features.Mediator.Commands.StockItemCommands;
using MultiShop.Services.Stock.Core.Domain.SeedWork;
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
        IStockItemRepository stockItemRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateStockItemCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateStockItemCommand request, CancellationToken cancellationToken)
        {
            var mapValue = mapper.Map<StockItem>(request);
            await stockItemRepository.AddAsync(mapValue);
            await unitOfWork.SaveChangesAsync();
            return "Stock olusturuldu";
        }
    }
}
