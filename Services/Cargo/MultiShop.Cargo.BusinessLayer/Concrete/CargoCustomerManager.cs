using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using MultiShop.Shared.Responses;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCustomerManager : ICargoCustomerService
    {
        private readonly ICargoCustomerDal _cargoCustomerDal;

        public CargoCustomerManager(ICargoCustomerDal _cargoCustomerDal)
        {
            this._cargoCustomerDal = _cargoCustomerDal;
        }

        public Result<string> TDelete(int id)
        {
            _cargoCustomerDal.Delete(id);
            return "Kargo müşterisi silindi";

        }

        public Result<List<CargoCustomer>> TGetAll()
        {
            return _cargoCustomerDal.GetAll();
        }

        public Result<CargoCustomer> TGetById(int id)
        {
            return _cargoCustomerDal.GetById(id);
        }

        public Result<CargoCustomer> TGetCargoCustomerById(string id)
        {
            return _cargoCustomerDal.GetCargoCustomerById(id);
        }

        public Result<List<CargoCustomer>> TGetCargoCustomerListById(string id)
        {
            return _cargoCustomerDal.GetCargoCustomerListById(id);

        }

        public Result<string> TInsert(CargoCustomer entity)
        {
            _cargoCustomerDal.Insert(entity);
            return "Kargo müşterisi oluşturuldu";

        }

        public Result<string> TUpdate(CargoCustomer entity)
        {
            _cargoCustomerDal.Update(entity);
            return "Kargo müşterisi kaydedildi";

        }
    }
}
