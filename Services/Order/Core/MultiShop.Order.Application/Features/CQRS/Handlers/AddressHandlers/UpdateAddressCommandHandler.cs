using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> _repository;
        public UpdateAddressCommandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }
        public async Task Handle(UpdateAddressCommand updateAddressCommand)
        {
            var value= await _repository.GetByIdAsync(updateAddressCommand.AddressId);
            value.UserId = updateAddressCommand.UserId;
            value.District=updateAddressCommand.District;
            value.City=updateAddressCommand.City;
            value.Detail1=updateAddressCommand.Detail1;
            value.Country = updateAddressCommand.Country;
            value.Description = updateAddressCommand.Description;
            value.Detail2 = updateAddressCommand.Detail2;
            value.Email = updateAddressCommand.Email;
            value.Name = updateAddressCommand.Name;
            value.Phone = updateAddressCommand.Phone;
            value.Surname = updateAddressCommand.Surname;
            value.ZipCode = updateAddressCommand.ZipCode;
            await _repository.UpdateAsync(value);
        }
    }
}
