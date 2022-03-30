using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;

namespace Movie.BO.Web.MVC.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
