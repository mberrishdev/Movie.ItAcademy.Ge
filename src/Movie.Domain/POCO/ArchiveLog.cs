using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.POCO
{
    public class ArchiveLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string LogEvent { get; set; }
    }
}
