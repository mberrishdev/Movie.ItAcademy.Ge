using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Web.Services.Abstractions
{
    public interface IBookingService
    {
        Task BookRoomAsync(Guid roomId, Guid userId);
        Task<bool> IsExistAsync(Guid bookingId);
    }
}
