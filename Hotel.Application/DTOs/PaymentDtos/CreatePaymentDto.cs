using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Application.DTOs.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; } = string.Empty;
    }
}
