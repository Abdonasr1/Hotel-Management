using CMS.Core.Entities;
using Hotel.Domain.Interfaces;
using Hotel.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public PaymentRepository(HotelDbContext context) : base(context)
        {
        }
    }
}
