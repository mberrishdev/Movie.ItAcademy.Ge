using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    internal class MessageLogsConfiguration : IEntityTypeConfiguration<MessageLog>
    {
        public void Configure(EntityTypeBuilder<MessageLog> builder)
        {
            builder.ToTable("MessageLog", "log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SendDate).HasColumnType("datetime");
        }
    }
}
