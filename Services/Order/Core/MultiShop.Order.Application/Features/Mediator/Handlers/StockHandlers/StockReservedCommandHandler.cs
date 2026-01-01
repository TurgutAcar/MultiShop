//using MediatR;
//using MultiShop.Order.Application.Features.Mediator.Commands.StockCommands;
//using MultiShop.Order.Domain.OrderAggregate;
//using MultiShop.Order.Domain.SeedWork;

//namespace MultiShop.Order.Application.Features.Mediator.Handlers.StockHandlers
//{
//    internal sealed class StockReservedCommandHandler(
//        IRepository<Ordering> _orderRepository,
//        IUnitOfWork _unitOfWork
//        )
//    : IRequestHandler<StockReservedCommand>
//    {
   

//        public async Task Handle(
//            StockReservedCommand request,
//            CancellationToken cancellationToken)
//        {
//            //var saga = await _sagaRepository
//            //    .GetByIdAsync(request.SagaId);

//            //if (saga == null) return;

//            //saga.MarkItemReserved(request.ProductId, request.Quantity);

//            ////if (saga.IsAllStockReserved())
//            ////{
//            ////    saga.Status = SagaStatus.StockReserved;

//            ////    var order = await _orderRepository
//            ////        .GetByIdAsync(request.OrderId);

//            ////   // order.Status = OrderStatus.StockReserved;
//            ////}

//            //await _unitOfWork.SaveChangesAsync();
//        }
//    }

//}
