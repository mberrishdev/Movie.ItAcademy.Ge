using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie.Domain.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Persistance.Configuration
{
    public class MessageQueueConfiguration : IEntityTypeConfiguration<MessageQueue>
    {
        public void Configure(EntityTypeBuilder<MessageQueue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).HasColumnType("datetime");
        }
    }
}
