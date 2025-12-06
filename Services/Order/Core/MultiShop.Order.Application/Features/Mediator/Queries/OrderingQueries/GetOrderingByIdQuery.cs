using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MultiShop.Order.Application.Features.Mediator.Behaviours;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
    public class GetOrderingByIdQuery:IRequest<Result<GetOrderingByIdQueryResult>>, IAuthorizeResourceRequest<int>
    {
        public int Id {  get; set; }
        public int ResourceId
        {
            get => Id;
            set => Id = value; // Setter zorunlu, ancak değeri OrderId'den alacak
        }
        public GetOrderingByIdQuery(int id)
        {
            Id = id;
        }
       
    }
}
