using MultiShop.Shared.Responses;

namespace MultiShop.Comment.BusinessLayer.Abstract
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
