using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Persistance.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Domain.POCO.Booking>
    {
        public void Configure(EntityTypeBuilder<Domain.POCO.Booking> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("UniqueIdentifier");
            builder.Property(x => x.UserId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.MovieId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.BookedDT).IsRequired();

            builder.HasOne(a => a.Payment).WithOne(b => b.Booking)
                .HasForeignKey<Movie.Domain.POCO.Payment>(e => e.BookingId);
        }
    }
}