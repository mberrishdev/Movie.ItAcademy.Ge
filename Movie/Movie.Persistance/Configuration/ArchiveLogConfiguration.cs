using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    internal class ArchiveLogConfiguration : IEntityTypeConfiguration<ArchiveLog>
    {
        public void Configure(EntityTypeBuilder<ArchiveLog> builder)
        {
            builder.ToTable("ArchiveLogs", "log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Timestamp).HasColumnType("datetime");
        }
    }
}
