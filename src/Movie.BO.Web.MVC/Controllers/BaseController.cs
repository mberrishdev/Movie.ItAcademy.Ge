using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Movie.BO.Web.MVC.Controllers
{
    public class BaseController : Controller
    {
        private IAntiforgery AntiForgery { get; set; }
        public BaseController(IAntiforgery antiForgery)
        {
            AntiForgery = antiForgery;
        }

        protected async Task<bool> IsAntiForgeryTokenValid()
        {
            try
            {
                await AntiForgery.ValidateRequestAsync(HttpContext);
                return true;
            }
            catch (AntiforgeryValidationException)
            {
                return false;
            }
        }
    }
}
