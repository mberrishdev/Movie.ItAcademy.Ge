using Movie.Services.Abstractions;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Movie.Services.Implementations
{
    public class HttpRequestServices : IHttpRequestServices
    {
        public async Task Get(string path)
        {
            var request = (HttpWebRequest)WebRequest.Create($"{path}");

            var response = await request.GetResponseAsync();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        }

        public async Task Post(string path)
        {
            var request = (HttpWebRequest)WebRequest.Create($"{path}");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var response = await request.GetResponseAsync();
        }

    }
}
