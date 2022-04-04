using Microsoft.EntityFrameworkCore;
using Movie.Persistance.Context;
using Movie.Services.Abstractions;
using Movie.Worker.Services.Abstractions;
using System.Threading.Tasks;

namespace Movie.Worker.Services.Implementations
{
    public class WebServices : IWebServices
    {
        public readonly IHttpRequestServices _httpRequestServices;

        public WebServices(IHttpRequestServices httpRequestServices)
        {
            _httpRequestServices = httpRequestServices;
        }

        public async Task RelodeWebData(MovieDBContext dBContext)
        {
            var urlMVC = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "move.web.mvc.relode");
            var urlAPI = await dBContext.ServerOptions.FirstOrDefaultAsync(op => op.Key == "move.web.api.relode");

            await _httpRequestServices.Post(urlMVC.Value);
            await _httpRequestServices.Post(urlAPI.Value);
        }
    }
}
