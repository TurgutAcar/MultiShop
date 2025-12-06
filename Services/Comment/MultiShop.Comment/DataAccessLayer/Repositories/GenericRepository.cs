using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.DataAccessLayer.Abstract;

namespace MultiShop.Comment.DataAccessLayer.Repositories
{


    public class GenericRepository<TEntity, TContext> : IGenericDal<TEntity> where TEntity : class
        where TContext:DbContext
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public void Delete(int id)
        {
            var values = _dbSet.Find(id);
            _dbSet.Remove(values);
            _context.SaveChanges();
        }

        public List<TEntity> GetAll()
        {
            var values = _dbSet.ToList();
            return values;
        }

        public TEntity GetById(int id)
        {
            var values = _dbSet.Find(id);
            return values;
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }

}
