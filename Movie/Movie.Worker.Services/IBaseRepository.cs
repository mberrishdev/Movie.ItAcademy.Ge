using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker
{
    public interface IBaseRepository
    {
        Task<List<T>> GetAllAsync<T>() where T : class;

        Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> GetAsync<T>(params object[] key) where T : class;

        Task AddAsync<T>(T entity) where T : class;

        Task RemoveAsync<T>(T entity) where T : class;

        Task RemoveAsync<T>(params object[] Key) where T : class;

        Task UpdateAsync<T>(T entity) where T : class;

        Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        Task<T> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

    }
}
