using System.Text;

namespace Domain.Utilities
{
    public class HttpClientUtility
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<HttpResponseMessage> SendJsonAsync(string? json, string url, string method)
        {
            var httpMethod = new HttpMethod(method);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(httpMethod, url)
            {
                Content = content,
            };

            Console.WriteLine("trimit pe link-ul " + url); 
            var a = await _httpClient.SendAsync(request);
            return a;
        }
    }
}
