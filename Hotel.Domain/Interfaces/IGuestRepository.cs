using Hotel.Domain.Entities;
using System.Linq.Expressions;

namespace Hotel.Domain.Interfaces
{
    public interface IGuestRepository : IRepository<Guest>
    {
        Task<Guest?> GetByPhoneOrEmailAsync(string? phone, string? email, params Expression<Func<Guest, object>>[] includes);
    }
}
