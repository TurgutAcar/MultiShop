using MultiShop.Order.Domain.Enums;
using MultiShop.Order.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Domain.OrderSagaAggregate
{
    //public class OrderSaga :  IAggregateRoot
    //{
    //    public int SagaId;
    //    public int OrderId { get; private set; }
    //    public SagaStatus Status { get; set; }
    //    public DateTime CreatedAt { get; private set; }

    //    private readonly List<OrderSagaItem> _items = new();
    //    public IReadOnlyCollection<OrderSagaItem> Items => _items;

    //    protected OrderSaga() { }

    //    public OrderSaga(int orderId)
    //    {
    //        OrderId = orderId;
    //        Status = SagaStatus.Started;
    //        CreatedAt = DateTime.UtcNow;
    //    }

    //    // 🔥 DOMAIN BEHAVIOR
    //    public void AddItem(string productId, int quantity)
    //    {
    //        _items.Add(new OrderSagaItem(productId, quantity));
    //    }

    //    public void MarkItemReserved(string productId, int reservedQuantity)
    //    {
    //        var item = _items.FirstOrDefault(x => x.ProductId == productId);
    //        if (item == null)
    //            //throw new DomainException("Saga item not found");

    //        item.MarkReserved(reservedQuantity);
    //    }

    //    public bool IsAllStockReserved()
    //    {
    //        return _items.All(x => x.IsReserved);
    //    }
    //}

}
