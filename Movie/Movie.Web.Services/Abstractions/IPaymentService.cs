using System;
using System.Threading.Tasks;

namespace Movie.Web.Services.Abstractions
{
    public interface IPaymentService
    {
        Task CreatePaymentAsync(Guid bookingId, Guid userId);
    }
}
