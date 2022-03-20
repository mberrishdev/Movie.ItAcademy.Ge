using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IMovieService
    {
        Task<Models.Movie> GetMovieAsync(Guid id);
        Task<List<Models.Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Models.Movie movie);
        Task UpdateMovieAsync(Models.Movie movie);
        Task DeleteMovieAsync(Guid id);
        Task ChangeMovieStatusAsync(Guid id, MovieStatus newStatus);
    }
}
