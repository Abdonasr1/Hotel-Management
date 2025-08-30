using CMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hotel.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.PaymentDate).IsRequired();

            builder.Property(p => p.Method)
                   .IsRequired()
                   .HasMaxLength(30);

            builder.HasOne(p => p.Reservation)
                   .WithMany()
                   .HasForeignKey(p => p.ReservationId);
        }
    }
}
