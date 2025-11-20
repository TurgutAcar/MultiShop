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
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetailDal;

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            _cargoDetailDal = cargoDetailDal;
        }

        public Result<string> TDelete(int id)
        {
            _cargoDetailDal.Delete(id);
            return "Kargo Detayı silindi";

        }

        public Result<List<CargoDetail>> TGetAll()
        {
            var values=_cargoDetailDal.GetAll();
            return values;
        }

        public Result<CargoDetail> TGetById(int id)
        {
            var value= _cargoDetailDal.GetById(id);
            return value;
        }

        public Result<string> TInsert(CargoDetail entity)
        {
            _cargoDetailDal.Insert(entity);
            return "Kargo müşterisi oluşturuldu";

        }

        public Result<string> TUpdate(CargoDetail entity)
        {
            _cargoDetailDal.Update(entity);
            return "Kargo müşterisi kaydedildi";


        }
    }
}
