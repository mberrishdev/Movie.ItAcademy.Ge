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
            var option = await _serverOptionService.GetOptionAsync("move.web.mvc.domain");
            var domain = option.Value;

            await _httpRequestServices.Post($"{domain}Web");
        }
    }
}
