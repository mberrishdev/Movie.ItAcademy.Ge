using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Implementations;
using Movie.BO.Web.MVC.Models.Room;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    [Authorize(Roles = "Moderator")]
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

        public async Task<IActionResult> Index(RoomWithMovieDTO model = null)
        {
            if (model.Id != Guid.Empty)
                View(model);

            List<Services.Models.Room> result = await _roomService.GetAllRoomsAsync();

            if (result == null)
                return NotFound();

            var rooms = result.Adapt<List<RoomDTO>>();

            return View(rooms);
        }

        public async Task<IActionResult> RoomDetails(Guid id)
        {
            Services.Models.Room result = await _roomService.GetRoomWithMovieAsync(id);

            if (result == null)
                return NotFound();

            var roomWithMovie = result.Adapt<RoomWithMovieViewModel>();

            return View(roomWithMovie);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            Services.Models.Room result = await _roomService.GetRoomWithMovieAsync(id);

            if (result == null)
                return NotFound();

            var roomWithMovie = result.Adapt<RoomWithMovieViewModel>();

            return View(roomWithMovie);
        }

        [HttpPost]
        public async Task<IActionResult> AddRoom(RoomCreateModel room)
        {
            if (!ModelState.IsValid)
                return View();

            var roomId = await _roomService.AddRoomAsync(new Services.Models.Room()
            {
                PremierTime = room.PremierTime,
                RoomUserCapacity = room.RoomUserCapacity,
                Price = room.Price,
                Currency = room.Currency,
            });

            return RedirectToAction("AddMovie", "Movie", new {
                roomId = roomId
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoomDetails(RoomWithMovieDTO model)
        {
            await _roomService.UpdateRoomAsync(model.Adapt<Services.Models.Room>());

            return RedirectToAction("RoomDetails", new
            {
                id = model.Id,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ActivateRoom(Guid id)
        {
            await _roomService.ChangeRoomStatusAsync(id, RoomStatus.Active);

            return RedirectToAction("RoomDetails", new
            {
                id = id,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InActivateRoom(Guid id)
        {
            await _roomService.ChangeRoomStatusAsync(id, RoomStatus.InActive);

            return RedirectToAction("RoomDetails", new
            {
                id = id,
            });
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _roomService.DeleteRoomAsync(id);

            return RedirectToAction("Index");
        }
    }
}
