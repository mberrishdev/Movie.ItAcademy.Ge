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

        public async Task AddMovieAsync(Domain.POCO.Movie movie)
        {
            await _baseRepository.AddAsync(movie);
        }

        public async Task UpdateMovieAsync(Domain.POCO.Movie movie)
        {
            await _baseRepository.UpdateAsync(movie);
        }
    }
}