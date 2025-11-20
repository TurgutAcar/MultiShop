using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public sealed class UpdateAddressCommandHandler(
         IRepository<Address> _repository,
         IMapper _mapper,
         IUnitOfWork unitOfWork
        )
    {
       

        public async Task Handle(UpdateAddressCommand updateAddressCommand)
        {
            var value= await _repository.GetByIdAsync(updateAddressCommand.AddressId);
            var mapper = _mapper.Map<Address>(value);

            await _repository.UpdateAsync(mapper);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
