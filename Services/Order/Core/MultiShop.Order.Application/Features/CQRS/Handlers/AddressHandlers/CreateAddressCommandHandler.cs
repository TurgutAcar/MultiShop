using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public sealed class CreateAddressCommandHandler(
        IRepository<Address> _repository,
        IMapper _mapper,
        IUnitOfWork _unitOfWork
      )
    {
        public async Task<Result<string>> Handle(CreateAddressCommand createAddressCommand )
        {
            var mapper = _mapper.Map<Address>(createAddressCommand);
            await _repository.CreateAsync(mapper);
            await _unitOfWork.SaveChangesAsync();
            return "Address oluşturuldu";
        }
    }
}
