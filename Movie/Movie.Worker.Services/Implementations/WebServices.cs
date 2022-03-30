using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class WebServices : IWebServices
    {
        public readonly IHttpRequestServices _httpRequestServices;
        public readonly Movie.Services.Abstractions.IServerOptionService _serverOptionService;

        public WebServices(IHttpRequestServices httpRequestServices, Movie.Services.Abstractions.IServerOptionService serverOptionService)
        {
            _httpRequestServices = httpRequestServices;
            _serverOptionService = serverOptionService;
        }

        public async Task RelodeWebData()
        {

            var urlMVC = await _serverOptionService.GetOptionAsync("move.web.mvc.relode");
            var urlAPI = await _serverOptionService.GetOptionAsync("move.web.api.relode");

            await _httpRequestServices.Post(urlMVC.Value);
            await _httpRequestServices.Post(urlAPI.Value);
        }
    }
}
