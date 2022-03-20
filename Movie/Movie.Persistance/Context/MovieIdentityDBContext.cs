using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Persistance.Context
{
    public class MovieIdentityDBContext : IdentityDbContext
    {
        public MovieIdentityDBContext(DbContextOptions<MovieIdentityDBContext> options) : base(options)
        {
        }

    }
}
