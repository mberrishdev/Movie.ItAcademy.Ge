using Microsoft.AspNetCore.Mvc;

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
