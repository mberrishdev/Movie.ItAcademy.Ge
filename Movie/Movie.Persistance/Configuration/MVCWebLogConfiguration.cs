﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Persistance.Configuration
{
    internal class MVCWebLogConfiguration : IEntityTypeConfiguration<MVCWebLog>
    {
        public void Configure(EntityTypeBuilder<MVCWebLog> builder)
        {
            builder.ToTable("MVCWebLogs", "log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Timestamp).HasColumnType("datetime");
        }
    }
}
