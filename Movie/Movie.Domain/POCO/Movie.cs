using System;
using System.Collections.Generic;
using System.Text;

namespace Movie.Domain.POCO
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gener { get; set; }
        public string Director { get; set; }
        public decimal IMDBReiting { get; set; }
        public DateTime PremierTime { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string BannerUrl { get; set; }
        public string Status { get; set; }
    }
}
