using System.Threading.Tasks;

namespace Movie.BO.Services.Abstractions
{
    public interface IHttpRequestServices
    {
        Task Get(string path);
        Task Post(string path);
    }
}
