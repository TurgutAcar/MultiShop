using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Commands
{
    public class RemoveOrderingCommand: IRequest<Result<string>>
    {
        public int Id { get; set; }
        public RemoveOrderingCommand(int id)
        {
            Id = id;
        }
    }
}
