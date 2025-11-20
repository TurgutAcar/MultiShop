using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Concrete;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.DataAccessLayer.Repositories
{

    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly CargoContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(CargoContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(int id)
        {
            var values= _dbSet.Find(id);
            _dbSet.Remove(values);
            _context.SaveChanges();
        }

        public List<T> GetAll()
        {
            var values = _dbSet.ToList();
            return values;
        }

        public T GetById(int id)
        {
            var values = _dbSet.Find(id);
            return values;
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();

        }
    }
}
