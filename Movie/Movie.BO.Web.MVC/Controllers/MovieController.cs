using Mapster;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieCreateModel movie)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.AddMovieAsync(movie.Adapt<Services.Models.Movie>());
            return View(movie);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> UpdateMovie(MovieDTO movie)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.UpdateMovieAsync(movie.Adapt<Services.Models.Movie>());
            return View(movie);
        }
    }
}
