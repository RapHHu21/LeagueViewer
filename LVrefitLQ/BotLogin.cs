using Refit;
using System.Net;
using System.Text.Json;

namespace LVrefitLOLP
{
    public class BotLogin
    {
        private string _username;
        private string _password;
        private string? authToken;
        private string urlPath = "https://lol.fandom.com";
        public BotLogin(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public async Task GetToken(HttpClient cookieUrl)
        {           

            var api = RestService.For<I_LoginToken>(cookieUrl);
            
            try
            {
                var getToken = await api.LoginToken();

                Console.WriteLine(getToken.RequestMessage.RequestUri);

                if (getToken.IsSuccessStatusCode)
                {
                    Console.WriteLine(getToken.Content);
                    var jsonResponse = getToken.Content;
                    var parseJson = JsonDocument.Parse(jsonResponse);
                    var tokenTest = getToken.Query.To
                    string? origToken = parseJson.RootElement
                        .GetProperty("query")
                        .GetProperty("tokens")
                        .GetProperty("logintoken")
                        .GetString();

                    Console.WriteLine(origToken.EndsWith("\\"));
                    authToken = origToken;
                    Console.WriteLine(authToken);
                    
                }
                else
                {
                    Console.WriteLine(getToken.StatusCode);
                    Console.WriteLine(getToken.Error);                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task LogBot()
        {

            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            var cookieHttpUrl = new HttpClient(handler)
            {
                BaseAddress = new Uri(urlPath)
            };

            await GetToken(cookieHttpUrl);

            if(authToken != null)
            {

                var api = RestService.For<I_BotLogin>(cookieHttpUrl);

                try
                {
                    var botLogin = _username;
                    var botParams = new BotLoginParams
                    {
                        lgpassword = _password,
                        lgtoken = authToken,
                    };

                    Console.WriteLine(authToken.EndsWith("\\"));
                    var logIn = await api.BotLoginPost(botParams, botLogin);

                    Console.WriteLine(logIn.RequestMessage.RequestUri);

                    if (logIn.IsSuccessStatusCode)
                    {
                        //jakims hujem token ktory dostaje wyzej nie pasuje xd

                        Console.WriteLine(logIn.Content);
                        Console.WriteLine("pifko");
                        var receivedTokenVar = JsonDocument.Parse(logIn.Content);
                        string receivedToken = receivedTokenVar.RootElement
                            .GetProperty("login")
                            .GetProperty("token")
                            .ToString();
                        Console.WriteLine("wiadomosc po getProperty: ");
                        Console.WriteLine(receivedToken);
                        Console.WriteLine(receivedToken.EndsWith("\\"));
                        Console.WriteLine(receivedToken == authToken);

                    }
                    else
                    {
                        Console.WriteLine(logIn.StatusCode);
                        Console.WriteLine("piwo2");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.WriteLine("piwo3");
                }
            }
            else
            {
                Console.WriteLine("auth token == null");
            }
        }
    }
}
