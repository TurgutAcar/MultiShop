using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Shared.Responses;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface ICargoCustomerService:IGenericService<CargoCustomer>
    {
        Result<CargoCustomer> TGetCargoCustomerById(string id);
        Result<List<CargoCustomer>> TGetCargoCustomerListById(string id);

    }
}
