using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MediaWikiApi.Requests {
    class WebClient {

        public static async Task<string> Request(Uri uri) {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            client.DefaultRequestHeaders.Accept.Clear();
            return await client.GetStringAsync(uri.AbsoluteUri);
        }
    }
}
