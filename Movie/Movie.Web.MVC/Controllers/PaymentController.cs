using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Web.MVC.Controllers
{
    public class PaymentController : BaseController
    {
        public readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, IAccountService accountController) : base(accountController)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        public async Task<IActionResult> CreatePayment(Guid bookingId)
        {
            IdentityUser user = await GetUserAsync();

            await _paymentService.CreatePaymentAsync(bookingId, new Guid(user.Id));

            return View();
        }
    }
}
