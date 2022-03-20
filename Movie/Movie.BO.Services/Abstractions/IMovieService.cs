using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IMovieService
    {
        Task AddMovieAsync(Models.Movie movie);
        Task UpdateMovieAsync(Models.Movie movie);
    }
}
