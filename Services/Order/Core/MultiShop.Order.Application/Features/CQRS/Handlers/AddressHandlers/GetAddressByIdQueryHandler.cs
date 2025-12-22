
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public sealed class GetAddressByIdQueryHandler(
        IRepository<Address> _repository,
        IMapper _mapper)
    {
      
        public async Task<Result<GetAddressByIdQueryResult>> Handle(GetAddressByIdQuery query)
        {
            var values = await _repository.GetByIdAsync(query.Id);
            var map=_mapper.Map<GetAddressByIdQueryResult>(values);
            return map;
          
        }
    }
}
