﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.POCO
{
    public class BOWebLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
    }
}
