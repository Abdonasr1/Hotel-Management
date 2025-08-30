using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hotel.Infrastructure.Persistence.Configurations
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Number)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(r => r.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(r => r.PricePerNight)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(r => r.IsAvailable)
                .HasDefaultValue(true);

            builder.HasMany(r => r.Reservations).WithOne(r => r.Room);

        }

    }
}
