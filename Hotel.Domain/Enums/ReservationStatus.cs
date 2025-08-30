

namespace Hotel.Domain.Enims
{
    public enum ReservationStatus
    {
        Pending,    // Reservation is pending confirmation
        Confirmed,  // Reservation is confirmed
        CheckedIn,  // Guest has checked in
        CheckedOut, // Guest has checked out
        Cancelled   // Reservation has been cancelled
    }
}