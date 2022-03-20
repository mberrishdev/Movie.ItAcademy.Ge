using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services;
using Movie.BO.Services.Abstractions;
using Movie.BO.Web.MVC.Models.Movie;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    public class MovieController : Controller
    {
        public readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        public async Task<IActionResult> MovieDetails(Guid id)
        {
            var result = await _movieService.GetMovieAsync(id);

            if (result == null)
                return NotFound();

            var movie = result.Adapt<MovieDTO>();

            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> ActivateMovie(Guid id)
        {
            await _movieService.ChangeMovieStatusAsync(id, MovieStatus.Active);

            return RedirectToAction("MovieDetails", new
            {
                id = id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> InActivateMovie(Guid id)
        {
            await _movieService.ChangeMovieStatusAsync(id, MovieStatus.InActive);

            return RedirectToAction("MovieDetails", new
            {
                id = id,
            });
        }


        public async Task<IActionResult> Movies()
        {
            var result = await _movieService.GetAllMoviesAsync();

            if (result == null)
                return NotFound();

            var movies = result.Adapt<List<MovieDTO>>();

            return View(movies);

        }

        [HttpGet]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            await _movieService.GetMovieAsync(id);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            await _movieService.GetAllMoviesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieCreateModel movie)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.AddMovieAsync(movie.Adapt<Services.Models.Movie>());
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie(MovieDTO movie)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.UpdateMovieAsync(movie.Adapt<Services.Models.Movie>());
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction("Movies");
        }
    }
}
