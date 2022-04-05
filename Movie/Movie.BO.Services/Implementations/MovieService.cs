using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.Data;
using System.Threading.Tasks;
namespace Movie.BO.Services.Implementations
{
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;
        public readonly IWebServices _webServices;

        public MovieService(IMovieRepository movieRepository, IWebServices webServices)
        {
            _movieRepository = movieRepository;
            _webServices = webServices;
        }

        public async Task AddMovieAsync(Models.Movie movie)
        {
            Domain.POCO.Movie movieModel = movie.Adapt<Domain.POCO.Movie>();

            await _movieRepository.AddMovieAsync(movieModel);

            //Relode web data
            await _webServices.RelodeWebData();
        }

        public async Task UpdateMovieAsync(Models.Movie movie)
        {
            await _movieRepository.UpdateMovieAsync(movie.Adapt<Domain.POCO.Movie>());

            //Relode web data
            await _webServices.RelodeWebData();
        }
    }
}
