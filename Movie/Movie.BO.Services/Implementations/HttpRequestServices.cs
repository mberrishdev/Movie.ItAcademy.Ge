using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using System.Text.Json;
using System;
using Movie.BO.Services.Abstractions;
using Movie.BO.Services.Models;

namespace Movie.BO.Services.Implementations
{
    public class HttpRequestServices : IHttpRequestServices
    { 
        private readonly IServerOptionService _serverOptionService;
        private const string domainKey = "move.web.mvc.domain";

        public HttpRequestServices(IServerOptionService serverOptionService)
        {
            _serverOptionService = serverOptionService;
        }

        public async Task Get(string path)
        {
            ServerOption option = await _serverOptionService.GetOptionAsync(domainKey);

            var request = (HttpWebRequest)WebRequest.Create($"{option.Value}{path}");

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public async Task Post(string path)
        {
            ServerOption option = await _serverOptionService.GetOptionAsync(domainKey);

            var request = (HttpWebRequest)WebRequest.Create($"{option.Value}{path}");

           // var postData = "thing1=" + Uri.EscapeDataString("hello");
            //postData += "&thing2=" + Uri.EscapeDataString("world");
           // var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
           // request.ContentLength = data.Length;

          //  using (var stream = request.GetRequestStream())
           // {
           //     stream.Write(data, 0, data.Length);
           // }

            var response = await request.GetResponseAsync();
        }

    }
}
