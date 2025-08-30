using Hotel.Domain.Entities;

using System.Linq.Expressions;

namespace Hotel.Domain.Interfaces
{
    public interface IReservationRepository : IRepository<Reservation>
    {
        Task<Reservation> SearchReservationsAsync(int? guestId,int? roomId, params Expression<Func<Reservation, object>>[] includes);
    }
}
