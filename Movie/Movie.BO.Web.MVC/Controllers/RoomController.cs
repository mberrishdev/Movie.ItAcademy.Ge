using Mapster;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Implementations;
using Movie.BO.Web.MVC.Models.Room;
using Movie.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PagedList;

namespace Movie.BO.Web.MVC.Controllers
{
    [Authorize(Roles = "Moderator")]
    public class RoomController : BaseController
    {
        public readonly IRoomService _roomService;

        public RoomController(IRoomService roomService, IAntiforgery antiForgery) : base(antiForgery)
        {
            _roomService = roomService;
        }

        public IActionResult AddRoom()
        {
            return View();
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page, RoomWithMovieDTO model = null)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "PremierTime_desc" : "PremierTime";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (model.Id != Guid.Empty)
                View(model);

            List<Services.Models.Room> result = await _roomService.GetAllRoomWithMovieAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(r => r.Movie.Name.Contains(searchString)).ToList();
            }

            if (result == null)
                return NotFound();

            var rooms = result.Adapt<List<RoomViewModel>>();

            rooms = sortOrder switch
            {
                "status_desc" => rooms.OrderByDescending(r => r.Status).ToList(),
                "PremierTime" => rooms.OrderBy(s => s.PremierTime).ToList(),
                "PremierTime_desc" => rooms.OrderByDescending(s => s.PremierTime).ToList(),
                _ => rooms.OrderBy(s => s.Status).ToList(),
            };

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(rooms.ToPagedList(pageNumber, pageSize));
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
        public async Task<IActionResult> AddRoom(RoomDTO room)
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
