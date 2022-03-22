using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("UniqueIdentifier");
            builder.Property(x => x.UserId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.RoomId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.BookedDT).IsRequired();
            builder.Property(x => x.PaymentStatus).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(25); ;


            builder.HasOne(a => a.Payment).WithOne(b => b.Booking)
                .HasForeignKey<Payment> (e => e.BookingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}