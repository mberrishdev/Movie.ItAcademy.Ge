using Movie.BO.Services.Abstractions;
using Movie.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.BO.Services.Implementations
{
    public class WebServices : IWebServices
    {
        public readonly IHttpRequestServices _httpRequestServices;
        public readonly IServerOptionService _serverOptionService;

        public WebServices(IHttpRequestServices httpRequestServices, IServerOptionService serverOptionService)
        {
            _httpRequestServices = httpRequestServices;
            _serverOptionService = serverOptionService;
        }

        public async Task RelodeWebData()
        {
            var urlMVC = await _serverOptionService.GetOptionAsync(MovieBOMVCResources.MoveWebMvcRelodeURL);
            var urlAPI = await _serverOptionService.GetOptionAsync(MovieBOMVCResources.MoveWebAPIRelodeURL);

            await _httpRequestServices.Post(urlMVC.Value);
            await _httpRequestServices.Post(urlAPI.Value);
        }
    }
}
