﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IWebServices
    {
        Task RelodeWebData();
    }
}