namespace Movie.Web.API.Services.Models
{
    public class JwtConfiguration
    {
        public string Secret { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
