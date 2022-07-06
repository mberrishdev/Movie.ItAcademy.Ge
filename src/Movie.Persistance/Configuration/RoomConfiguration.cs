using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.Rooms;

namespace Movie.Persistance.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            builder.Property(x => x.PremierTime).IsRequired();
            builder.Property(x => x.RoomUserCapacity).IsRequired();
            builder.Property(x => x.DurationMinutes).IsRequired();
            builder.Property(x => x.UserCount).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.Status).IsRequired();


            builder.HasOne(a => a.Movie).WithOne(b => b.Room)
                .HasForeignKey<Domain.POCO.Movie>(e => e.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}