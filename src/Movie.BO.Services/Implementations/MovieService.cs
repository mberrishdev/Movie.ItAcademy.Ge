using Movie.BO.Services.Abstractions;
using Movie.Data;
using Movie.Domain.Movie.Commands;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly IBaseRepository<Domain.Movie.Movie> _baseRepository;
        public readonly IWebServices _webServices;

        public MovieService(IWebServices webServices, IBaseRepository<Domain.Movie.Movie> baseRepository)
        {
            _webServices = webServices;
            _baseRepository = baseRepository;
        }

        public async Task AddMovieAsync(CreateMovieCommand command)
        {
            var movie = new Domain.Movie.Movie(command);

            await _baseRepository.AddAsync(movie);
            await _webServices.RelodeWebData();
        }

        //public async Task UpdateMovieAsync(Models.Movie movie)
        //{
        //    //await _movieRepository.UpdateMovieAsync(movie.Adapt<Domain.POCO.Movie>());

        //    ////Relode web data
        //    //await _webServices.RelodeWebData();
        //}
    }
}
