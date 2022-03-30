﻿using Movie.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Abstractions
{
    public interface IBookingService
    {
        Task CheckAndCancellBookings(MovieDBContext dBContext);
    }
}
