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
    public class CargoCompanyManager :ICargoCompanyService
    {
        private readonly ICargoCompanyDal _cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            _cargoCompanyDal = cargoCompanyDal;
        }

        public Result<string> TDelete(int id)
        {
            _cargoCompanyDal.Delete(id);
            return "Kargo şirketi silindi";
        }

        public Result<List<CargoCompany>> TGetAll()
        {
           return _cargoCompanyDal.GetAll();
        }

        public Result<CargoCompany> TGetById(int id)
        {
            return _cargoCompanyDal.GetById(id);    
        }

        public Result<string> TInsert(CargoCompany entity)
        {
            _cargoCompanyDal.Insert(entity);
            return "Kargo şirketi oluşturuldu";

        }

        public Result<string> TUpdate(CargoCompany entity)
        {
            _cargoCompanyDal.Update(entity);
            return "Kargo şirketi kaydedildi";

        }
    }
}
