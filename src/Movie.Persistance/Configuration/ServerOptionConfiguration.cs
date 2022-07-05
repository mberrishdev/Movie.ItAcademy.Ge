using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    public class ServerOptionConfiguration : IEntityTypeConfiguration<ServerOption>
    {
        public void Configure(EntityTypeBuilder<ServerOption> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("UniqueIdentifier");
        }
    }
}
