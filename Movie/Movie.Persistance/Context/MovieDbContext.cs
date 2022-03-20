using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieDBContext).Assembly);
        }

    }
}
