
namespace Hotel.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRoomRepository RoomRepository { get; }
        IGuestRepository GuestRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
