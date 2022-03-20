using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Services.Enums
{
    public enum RoomStatus
    {
        New,
        Active,
        InActive
    }

    public enum BookingStatus
    {
        Active,
        CancelledByModerator,
        CancelledByUser,
        CancelledByWorker
    }

    public enum PaymentStatus
    {
        Paid,
        Unpaid
    }
}
