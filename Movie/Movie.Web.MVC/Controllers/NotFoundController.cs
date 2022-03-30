using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;

namespace Movie.Web.MVC.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
