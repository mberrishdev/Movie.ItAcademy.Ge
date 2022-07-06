using Movie.Domain.Movie.Commands;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IMovieService
    {
        Task AddMovieAsync(CreateMovieCommand command);
        //Task UpdateMovieAsync(Models.Movie movie);
    }
}
