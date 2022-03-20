using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Persistance.Configuration
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Domain.POCO.Payment>
    {
        public void Configure(EntityTypeBuilder<Domain.POCO.Payment> builder)
        {
            builder.HasKey(x => x.BookingId);
            builder.Property(x => x.BookingId).HasColumnType("UniqueIdentifier");
            builder.Property(x => x.UserId).HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.PayemntDT).IsRequired();
        }
    }
}