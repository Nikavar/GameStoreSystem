using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Data.Infrastructure
{
    public interface IBaseRepository<T> where T : class    
    {
        #region General Methods for All Classes

        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetManyAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(params object[] key);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(Expression<Func<T, bool>> filter);
        Task SaveAsync();


        #endregion
    }
}
