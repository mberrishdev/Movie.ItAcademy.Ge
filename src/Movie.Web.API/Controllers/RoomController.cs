using Mapster;
using Microsoft.AspNetCore.Mvc;
using Movie.Web.API.Models;
using Movie.Web.API.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController
    {
        public readonly IRoomService _roomService;
        public RoomController(IRoomService roomService, IAccountService accountService) : base(accountService)
        {
            _roomService = roomService;
        }

        [HttpGet("GetRooms")]
        public async Task<IActionResult> GetRooms()
        {
            List<Web.Services.Models.Room> result = await _roomService.GetAllRoomWithMovieAsync();

            if (result == null)
                return NotFound();

            List<MovieSIViewModel> movieSmallInfo = result.Select(rm => new MovieSIViewModel()
            {
                Id = rm.Id,
                Name = rm.Movie.Name,
                BannerUrl = rm.Movie.BannerUrl,
            }).ToList();

            return Ok(movieSmallInfo);
        }

        [HttpGet("RoomDetails")]
        public async Task<IActionResult> RoomDetails(Guid id)
        {
            Web.Services.Models.Room result = await _roomService.GetRoomWithMovieAsync(id);

            if (result == null)
                return NotFound();

            var roomWithMovie = result.Adapt<RoomWithMovieViewModel>();

            return Ok(roomWithMovie);
        }
    }
}
