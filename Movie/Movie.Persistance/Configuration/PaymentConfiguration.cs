using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.BookingId);
            builder.Property(x => x.BookingId).HasColumnType("UniqueIdentifier");
            builder.Property(x => x.UserId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.PayemntDT).IsRequired();
        }
    }
}