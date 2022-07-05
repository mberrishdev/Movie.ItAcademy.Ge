using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Web.API.Services.Abstractions;
using Movie.Web.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace Movie.Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaymentController : BaseController
    {
        public readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, IAccountService accountController) : base(accountController)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(Guid bookingId)
        {
            IdentityUser user = await GetUserAsync();

            await _paymentService.CreatePaymentAsync(bookingId, new Guid(user.Id));

            return Ok();
        }
    }
}
