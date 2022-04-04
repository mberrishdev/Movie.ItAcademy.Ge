using Microsoft.EntityFrameworkCore;
using Movie.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker
{
    public class BaseRepository : IBaseRepository
    {
        private readonly DbContext _context;

        public BaseRepository(MovieDBContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetAsync<T>(params object[] key) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return await _dbSet.FindAsync(key);
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            if (entity == null)
                return;
            DbSet<T> _dbSet = _context.Set<T>();
            _dbSet.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync<T>(T entity) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync<T>(params object[] key) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            var entity = await GetAsync<T>(key);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            DbSet<T> _dbSet = _context.Set<T>();
            return await _dbSet.Where(predicate).ToListAsync();
        }
    }
}
