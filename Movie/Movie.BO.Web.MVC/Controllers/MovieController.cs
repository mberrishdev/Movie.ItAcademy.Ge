using Mapster;
using Microsoft.AspNetCore.Antiforgery;
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
    [AutoValidateAntiforgeryToken]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Moderator")]
    public class MovieController : BaseController
    {
        public readonly IMovieService _movieService;

        public MovieController(IMovieService movieService, IAntiforgery antiForgery) : base(antiForgery)
        {
            _movieService = movieService;
        }

        public IActionResult AddMovie(Guid roomId)
        {
            ViewData["RoomId"] = roomId;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieDTO movie)
        {
            if (!ModelState.IsValid)
                return View();

            await _movieService.AddMovieAsync(movie.Adapt<Services.Models.Movie>());
            return View(movie);
        }
    }
}
