using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hotel.Infrastructure.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {

            builder.HasKey(r => r.Id);

            builder.Property(r => r.CheckInDate).IsRequired();
            builder.Property(r => r.CheckOutDate).IsRequired();

            builder.Property(r => r.TotalPrice)
                   .IsRequired()
                   .HasColumnType("decimal(10,2)");

            builder.Property(r => r.Status)
                   .HasConversion<string>()
                   .IsRequired();

            builder.HasOne(r => r.Room)
                   .WithMany(rm => rm.Reservations)
                   .HasForeignKey(r => r.RoomId);

            builder.HasOne(r => r.Guest)
                   .WithMany(r => r.Reservations)
                   .HasForeignKey(r => r.GuestId);

        }
    }
}
