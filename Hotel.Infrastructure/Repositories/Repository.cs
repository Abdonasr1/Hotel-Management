using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly HotelDbContext _context;
        DbSet<T> _dbset;
        public Repository(HotelDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbset.AsQueryable();

            if (filter is not null)
                query = query.Where(filter);


            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbset.AsQueryable();

            if (filter is not null)
                query = query.Where(filter);


            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync();
        }

        public void RemoveAsync(T entityT)
        {
            _context.Remove(entityT);
        }

        public void UpdateAsync(T entity)
        {
            _context.Update(entity);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

    }
}
