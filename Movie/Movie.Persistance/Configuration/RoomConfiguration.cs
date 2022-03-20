using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movie.Persistance.Configuration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Domain.POCO.Room>
    {
        public void Configure(EntityTypeBuilder<Domain.POCO.Room> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PremierTime).IsRequired();
            builder.Property(x => x.RoomUserCapacity).IsRequired();
            builder.Property(x => x.UserCount).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.Status).IsRequired();

            builder.HasOne(a => a.Movie).WithOne(b => b.Room)
                .HasForeignKey<Movie.Domain.POCO.Movie>(e => e.RoomId);
        }
    }
}