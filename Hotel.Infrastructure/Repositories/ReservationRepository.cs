using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel.Infrastructure.Repositories
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        private readonly HotelDbContext _context;

        public ReservationRepository(HotelDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Reservation> SearchReservationsAsync(int? guestId, params Expression<Func<Reservation, object>>[] includes)
        {
            var query = _context.Reservations.AsQueryable();
            
            if (includes != null && includes.Length > 0)
                foreach (var include in includes)
                    query = query.Include(include);

            query = query.Where(r =>
                          (guestId.HasValue && r.GuestId == guestId.Value)
            );


            return await query.FirstOrDefaultAsync();
        }
    }
}
