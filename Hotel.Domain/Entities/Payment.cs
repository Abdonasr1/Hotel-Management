using Hotel.Domain.Entities;

namespace CMS.Core.Entities
{
    public class Payment
    {
        public int Id { get; set; }


        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }




        public decimal Amount { get; set; } 
        public DateTime PaymentDate { get; set; } 
        public string Method { get; set; } = string.Empty;
    }
}