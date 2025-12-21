using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Domain.Events
{
    public sealed record StockTransactionCreatedEvent(
         string ProductId,

     int Quantity,

     int TransactionTypeValue,

     int ReferenceId, // OrderId, GoodsReceiptId vs

     DateTime CreatedAt,
     string Description): INotification;



}
