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
    public class CargoOperationManager : ICargoOperationService
    {
        private readonly ICargoOperationDal _cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            _cargoOperationDal = cargoOperationDal;
        }

        public Result<string> TDelete(int id)
        {
            _cargoOperationDal.Delete(id);
            return "Kargo Operasyonu silindi";

        }

        public Result<List<CargoOperation>> TGetAll()
        {
           return _cargoOperationDal.GetAll();
        }

        public Result<CargoOperation> TGetById(int id)
        {
          return  _cargoOperationDal.GetById(id);
        }

        public Result<string> TInsert(CargoOperation entity)
        {
            _cargoOperationDal.Insert(entity);
            return "Kargo Operasyonu oluşturuldu";

        }

        public Result<string> TUpdate(CargoOperation entity)
        {
            _cargoOperationDal.Update(entity);
            return "Kargo Operasyonu kaydedildi";

        }
    }
}
