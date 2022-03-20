using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services;
using Movie.BO.Services.Abstractions;
using Movie.BO.Web.MVC.Models.Room;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    public class RoomController : Controller
    {
        public readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }


        public IActionResult AddRoom()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            List<Services.Models.Room> result = await _roomService.GetAllRoomsAsync();

            if (result == null)
                return NotFound();

            var rooms = result.Adapt<List<RoomDTO>>();

            return View(rooms);
        }

        //[Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomCreateModel room)
        {
            if (!ModelState.IsValid)
                return View();

            await _roomService.AddRoomAsync(new Services.Models.Room()
            {
                PremierTime = room.PremierTime,
                RoomUserCapacity = room.RoomUserCapacity,
                Price = room.Price,
                Currency = room.Currency,
            });

            return RedirectToAction("AddMovie", "Movie");
        }

        public async Task<IActionResult> RoomDetails(Guid id)
        {
            Services.Models.Room result = await _roomService.GetRoomWithMovieAsync(id);

            if (result == null)
                return NotFound();

            var roomWithMovie = result.Adapt<RoomWithMovieDTO>();

            return View(roomWithMovie);
        }


        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ActivateRoom(Guid id)
        {
            await _roomService.ChangeRoomStatusAsync(id, RoomStatus.Active);

            return RedirectToAction("RoomDetails", new
            {
                id = id,
            });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InActivateRoom(Guid id)
        {
            await _roomService.ChangeRoomStatusAsync(id, RoomStatus.InActive);

            return RedirectToAction("RoomDetails", new
            {
                id = id,
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            await _roomService.DeleteRoomAsync(id);

            return RedirectToAction("Index");
        }
    }
}
