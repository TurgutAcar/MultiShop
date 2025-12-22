using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public sealed class GetAddressQueryHandler(
        IRepository<Address> _repository,
        IMapper _mapper
        )
    {
       
        public async Task<Result<List<GetAddressQueryResult>>> Handle()
        {
            var values= await _repository.GetAllAsync();
            var mapList = values.Select(x => _mapper.Map<GetAddressQueryResult>(x)).ToList();
            return mapList;
          
        }
    }
}
