using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Data
{
    public interface IMovieRepository
    {
        Task<Domain.POCO.Movie> GetMovieAsync(Guid id);
        Task<List<Domain.POCO.Movie>> GetAllMoviesAsync();
        Task AddMovieAsync(Domain.POCO.Movie movie);
        Task UpdateMovieAsync(Domain.POCO.Movie movie);
        Task DeleteMovieAsync(Domain.POCO.Movie movie);
        Task ChangeMovieStatusAsync(Guid id,string newStatus);
    }
}
