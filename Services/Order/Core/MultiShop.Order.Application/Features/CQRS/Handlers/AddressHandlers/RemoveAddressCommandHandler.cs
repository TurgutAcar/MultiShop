using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.OrderAggregate;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Shared.Responses;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public sealed class RemoveAddressCommandHandler(
             IRepository<Address> _repository,
             IUnitOfWork unitOfWork

        )
    {

      
        public async Task<Result<string>> Handle(RemoveAddressCommand removeAddressCommand) {
            var value=await _repository.GetByIdAsync(removeAddressCommand.Id);
            await _repository.DeleteAsync(value);
            await unitOfWork.SaveChangesAsync();
            return "Address silindi";
        
        }
    }
}
