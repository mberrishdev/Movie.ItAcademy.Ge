using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    public class RoomArchiveConfiguration : IEntityTypeConfiguration<RoomArchive>
    {
        public void Configure(EntityTypeBuilder<RoomArchive> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
