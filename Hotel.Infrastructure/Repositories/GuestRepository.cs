using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel.Infrastructure.Repositories
{
    public class GuestRepository : Repository<Guest>, IGuestRepository
    {
        private readonly HotelDbContext _context;
        public GuestRepository(HotelDbContext context) : base(context)
        {
            _context = context;
        }
        



        public async Task<Guest?> GetByPhoneOrEmailAsync(string? phone, string? email, params Expression<Func<Guest, object>>[] includes)
        {
            var query =  _context.Guests.AsQueryable();
            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            query = query.Where(g =>
                    (!string.IsNullOrEmpty(phone) && g.Phone == phone) ||
                    (!string.IsNullOrEmpty(email) && g.Email == email));
            return await query.FirstOrDefaultAsync();
        }
    }
}
