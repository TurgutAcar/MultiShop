using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByIdQuery:IRequest<Result<GetOrderingByIdQueryResult>>
    {
        public int Id {  get; set; }
        public GetOrderingByIdQuery(int id)
        {
            Id = id;
        }
    }
}
