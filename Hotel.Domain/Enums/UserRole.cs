

namespace Hotel.Domain.Enims
{
    public enum UserRole
    {
        Guest,     // Limited access, can view own reservations
        Receptionist, // Can manage reservations and guests
        Admin      // Full access to all features
    }
}
