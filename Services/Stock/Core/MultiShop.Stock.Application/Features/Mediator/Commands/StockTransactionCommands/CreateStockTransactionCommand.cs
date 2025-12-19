using MediatR;
using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Services.Stock.Core.Application.Features.Mediator.Commands.StockTransactionCommands
{
    public sealed record CreateStockTransactionCommand(
        
     string ProductId ,

     int Quantity,

     int TransactionTypeValue ,

     int ReferenceId , // OrderId, GoodsReceiptId vs

     DateTime CreatedAt):IRequest<Result<string>>;
    
    
}
