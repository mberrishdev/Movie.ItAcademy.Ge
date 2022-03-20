using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.Data.EF.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IBaseRepository<Domain.POCO.Movie> _baseRepository;

        public MovieRepository(IBaseRepository<Domain.POCO.Movie> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<Domain.POCO.Movie> GetMovieAsync(Guid id)
        {
            return await _baseRepository.GetAsync(id);
        }

        public async Task<List<Domain.POCO.Movie>> GetAllMoviesAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task AddMovieAsync(Domain.POCO.Movie movie)
        {

            await _baseRepository.AddAsync(movie);
        }

        public async Task UpdateMovieAsync(Domain.POCO.Movie movie)
        {
            await _baseRepository.UpdateAsync(movie);
        }

        public async Task DeleteMovieAsync(Domain.POCO.Movie movie)
        {
            await _baseRepository.RemoveAsync(movie);
        }

        public async Task ChangeMovieStatusAsync(Guid id, string newStatus)
        {
            var movie = await _baseRepository.Table.FirstOrDefaultAsync(movie => movie.Id == id);
            movie.Status = newStatus;
            await _baseRepository.UpdateAsync(movie);
        }
    }
}