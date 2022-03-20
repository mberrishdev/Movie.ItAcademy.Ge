using Mapster;
using Movie.BO.Services.Abstractions;
using Movie.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Models.Movie> GetMovieAsync(Guid id)
        {
            var result = await _movieRepository.GetMovieAsync(id);

            return result.Adapt<Models.Movie>();
        }

        public async Task<List<Models.Movie>> GetAllMoviesAsync()
        {
            var result = await _movieRepository.GetAllMoviesAsync();

            return result.Adapt<List<Models.Movie>>();
        }

        public async Task AddMovieAsync(Models.Movie movie)
        {
            Domain.POCO.Movie movieModel = movie.Adapt<Domain.POCO.Movie>();

            movieModel.Status = MovieStatus.New.ToString();

            await _movieRepository.AddMovieAsync(movieModel);
        }

        public async Task UpdateMovieAsync(Models.Movie movie)
        {
            await _movieRepository.UpdateMovieAsync(movie.Adapt<Domain.POCO.Movie>());
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            var movie = GetMovieAsync(id);

            if(movie != null)
                await _movieRepository.DeleteMovieAsync(movie.Adapt<Domain.POCO.Movie>());
        }

        public async Task ChangeMovieStatusAsync(Guid id, MovieStatus newStatus)
        {
            await _movieRepository.ChangeMovieStatusAsync(id, newStatus.ToString());
        }
    }
}
