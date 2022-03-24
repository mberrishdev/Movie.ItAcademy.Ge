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

            // var postData = "thing1=" + Uri.EscapeDataString("hello");
            // postData += "&thing2=" + Uri.EscapeDataString("world");
            // var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            // request.ContentLength = data.Length;

            // using (var stream = request.GetRequestStream())
            // {
            //     stream.Write(data, 0, data.Length);
            // }

            var response = await request.GetResponseAsync();
        }

    }
}
