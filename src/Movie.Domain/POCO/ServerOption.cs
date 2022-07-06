using System;
using System.ComponentModel.DataAnnotations;

namespace Movie.Domain.POCO
{
    public class ServerOption
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string Attribute1 { get; set; }
        public string Attribute2 { get; set; }
        public string Attribute3 { get; set; }
        public string Attribute4 { get; set; }
    }
}
