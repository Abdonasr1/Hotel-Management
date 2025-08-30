
using System.Linq.Expressions;

namespace Hotel.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null!, params Expression<Func<T, object>>[] includes);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> filter = null!, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T entity);
        void UpdateAsync(T entity);
        void RemoveAsync(T entityT);
    }
}
