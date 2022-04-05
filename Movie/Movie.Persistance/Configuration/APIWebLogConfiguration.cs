﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;

namespace Movie.Persistance.Configuration
{
    internal class APIWebLogConfiguration : IEntityTypeConfiguration<APIWebLog>
    {
        public void Configure(EntityTypeBuilder<APIWebLog> builder)
        {
            builder.ToTable("APIWebLogs", "log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Timestamp).HasColumnType("datetime");
        }
    }
}
