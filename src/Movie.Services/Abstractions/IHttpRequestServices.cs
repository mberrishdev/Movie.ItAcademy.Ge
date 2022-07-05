using System.Threading.Tasks;

namespace Movie.Services.Abstractions
{
    public interface IHttpRequestServices
    {
        Task Get(string path);
        Task Post(string path);
    }
}
