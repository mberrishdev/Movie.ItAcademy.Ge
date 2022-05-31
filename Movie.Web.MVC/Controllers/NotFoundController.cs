using Microsoft.AspNetCore.Mvc;

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
