
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;

namespace Hotel.Infrastructure.Repositories
{
    public class UnitOfWork(HotelDbContext context,
        IPaymentRepository paymentRepository,
        IRoomRepository roomRepository,
        IGuestRepository guestRepository,
        IReservationRepository reservationRepository) : IUnitOfWork
    {
        private readonly HotelDbContext _context = context;

        public IPaymentRepository PaymentRepository { get; private set; } = paymentRepository;
        public IRoomRepository RoomRepository { get; private set; } = roomRepository;
        public IGuestRepository GuestRepository { get; private set; } = guestRepository;
        public IReservationRepository ReservationRepository { get; private set; } = reservationRepository;


        public void Dispose() => _context.Dispose();

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
