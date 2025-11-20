using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Commands
{
    public class CreateOrderingCommand:IRequest<Result<string>>
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
