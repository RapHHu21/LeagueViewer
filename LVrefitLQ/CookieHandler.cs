using System.Diagnostics;
using System.Net;

namespace LVrefitLOLP
{
    public class CookieHandler
    {
        public string _url;
        public CookieContainer cookies;
        public HttpClient client;
        
        public CookieHandler(string url)
        {
            _url = url;
            cookies = new CookieContainer();
            client = CreateCookieHandler(_url, cookies);
        }

        public static HttpClient CreateCookieHandler(string url, CookieContainer cookies)
        {            
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var cookieHttpUrl = new HttpClient(handler)
            {
                BaseAddress = new Uri(url)
            };

            return cookieHttpUrl;
        }

        public void ShowCookiesMSG(string componentName)
        {
            var cookiesCheck = cookies.GetCookies(new Uri(_url));
            foreach (Cookie ciacho in cookiesCheck)
            {
                Debug.WriteLine($"ciasteczki po {componentName} {ciacho.Value} = {ciacho.Domain}");
            }
        }
    }
}
