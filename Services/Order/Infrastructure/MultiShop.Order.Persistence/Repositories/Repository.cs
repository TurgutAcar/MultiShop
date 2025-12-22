using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MultiShop.Order.Domain.SeedWork;
using MultiShop.Order.Persistence.Context;

namespace MultiShop.Order.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
      
    {
        private readonly OrderContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(OrderContext context)
        {
            _context = context;
            _dbSet= _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
           // await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            //await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
           // await _context.SaveChangesAsync();
        }
    }
}
