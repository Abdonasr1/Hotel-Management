using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;

namespace Hotel.Infrastructure.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(HotelDbContext context) : base(context)
        {
        }

    }
}
