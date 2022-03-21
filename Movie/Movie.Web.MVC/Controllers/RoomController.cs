using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;
using Movie.Web.MVC.Models;
using Movie.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    public class RoomController : BaseController
    {
        public readonly IRoomService _roomService;
        public RoomController(IRoomService roomService, IAccountService accountController) : base(accountController)
        {
            _roomService = roomService;
        }

        public async Task<IActionResult> Index()
        {
            List<Services.Models.Room> result = await _roomService.GetAllRoomWithMovieAsync();

            if (result == null)
                return NotFound();

            List<MovieSIViewModel> movieSmallInfo = result.Select(rm => new MovieSIViewModel()
            {
                Id = rm.Id,
                Name = rm.Movie.Name,
                BannerUrl = rm.Movie.BannerUrl,
            }).ToList();

            return View(movieSmallInfo);
        }

        public async Task<IActionResult> RoomDetails(Guid id)
        {
            Services.Models.Room result = await _roomService.GetRoomWithMovieAsync(id);

            if (result == null)
                return NotFound();

            var roomWithMovie = result.Adapt<RoomWithMovieViewModel>();

            return View(roomWithMovie);
        }
    }
}
