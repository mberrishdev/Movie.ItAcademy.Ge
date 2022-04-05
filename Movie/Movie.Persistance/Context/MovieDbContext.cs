using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Movie.Persistance.Context
{
    public class MovieDBContext : IdentityDbContext
    {
        public MovieDBContext(DbContextOptions<MovieDBContext> options) : base(options)
        {
        }

        public DbSet<Domain.POCO.Movie> Movies { get; set; }
        public DbSet<Domain.POCO.Room> Rooms { get; set; }
        public DbSet<Domain.POCO.Booking> Bookings { get; set; }
        public DbSet<Domain.POCO.Payment> Payments { get; set; }
        public DbSet<Domain.POCO.RoomArchive> RoomArchives { get; set; }
        public DbSet<Domain.POCO.ServerOption> ServerOptions { get; set; }
        public DbSet<Domain.POCO.APIWebLog> APIWebLogs { get; set; }
        public DbSet<Domain.POCO.MVCWebLog> MVCWebLogs { get; set; }
        public DbSet<Domain.POCO.BOWebLog> BOWebLogs { get; set; }
        public DbSet<Domain.POCO.ArchiveLog> ArchiveLogs { get; set; }
        public DbSet<Domain.POCO.MessageQueue> MessageQueues { get; set; }
        public DbSet<Domain.POCO.MessageLog> MessageLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDBContext).Assembly);
        }
    }
}
