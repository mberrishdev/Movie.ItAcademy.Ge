using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    public class BOWebLogConfiguration : IEntityTypeConfiguration<BOWebLog>
    {
        public void Configure(EntityTypeBuilder<BOWebLog> builder)
        {
            builder.ToTable("BOWebLogs", "log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Timestamp).HasColumnType("datetime");
        }
    }
}
