using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hotel.Infrastructure.Persistence.Configurations
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {

            builder.HasKey(g => g.Id);

            builder.Property(g => g.FullName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(g => g.Phone)
                   .HasMaxLength(20);

            builder.Property(g => g.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(g => g.NationalId)
                   .HasMaxLength(20);

            builder.Property(g => g.Notes)
                     .HasMaxLength(500);
        }
    }
}
