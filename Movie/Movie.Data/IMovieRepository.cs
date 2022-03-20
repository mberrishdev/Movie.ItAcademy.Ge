﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IMovieRepository
    {
        Task AddMovieAsync(Domain.POCO.Movie movie);
        Task UpdateMovieAsync(Domain.POCO.Movie movie);
    }
}
