using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.POCO
{
    public class MessageLog
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string ContactAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Status { get; set; }
        public DateTime SendDate { get; set; }
    }
}
