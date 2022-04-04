using System;

namespace Movie.Domain.POCO
{
    public class MessageQueue
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string ContactAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
    }
}
