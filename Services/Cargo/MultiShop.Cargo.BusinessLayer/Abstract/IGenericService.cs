using MultiShop.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Result<string> TInsert(T entity);
        Result<string> TUpdate(T entity);
        Result<string> TDelete(int id);
        Result<T> TGetById(int id);
        Result<List<T>> TGetAll();
    }
}
