using Refit;
using System.Diagnostics;
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

                Debug.WriteLine(getToken.RequestMessage.RequestUri);

                if (getToken.IsSuccessStatusCode)
                {
                    Debug.WriteLine("");
                    Debug.WriteLine(getToken.Content);
                    var jsonResponse = getToken.Content;
                    var parseJson = JsonDocument.Parse(jsonResponse);
                    
                    string? origToken = parseJson.RootElement
                        .GetProperty("query")
                        .GetProperty("tokens")
                        .GetProperty("logintoken")
                        .GetString();

                    Debug.WriteLine(origToken.EndsWith("\\"));
                    authToken = origToken;
                    Debug.WriteLine(authToken);
                    Debug.WriteLine("token side");

                }
                else
                {
                    Debug.WriteLine(getToken.StatusCode);
                    Debug.WriteLine(getToken.Error);
                    Debug.WriteLine("token side");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Debug.WriteLine("token side");
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

            var cookiesCheck = cookies.GetCookies(new Uri(urlPath));
            
            foreach (Cookie ciacho in cookiesCheck)
            {
                Debug.WriteLine($"ciasteczki po tokenie {ciacho.Value} = {ciacho.Domain}");
            }


            if(authToken != null)
            {

                var api = RestService.For<I_BotLogin>(cookieHttpUrl);

                try
                {
                    var botLogin = _username;
                    Debug.WriteLine("czy token ma backslash: " + authToken.EndsWith("\\"));
                    var botParams = new BotLoginParams
                    {
                        lgpassword = _password,
                        lgtoken = authToken,
                    };
                    

                    Debug.WriteLine("czy token w klasie ma slash" + botParams.lgtoken.EndsWith("\\"));
                    var logIn = await api.BotLoginPost(botParams, botLogin);


                    Debug.WriteLine(logIn.RequestMessage.RequestUri);

                    if (logIn.IsSuccessStatusCode)
                    {
                        //Debug.WriteLine(logIn.Content.ReadAsString());
                        //jakims hujem token ktory dostaje wyzej nie pasuje xd

                        Debug.WriteLine(logIn.Content);
                        Debug.WriteLine("pifko");
                        var receivedTokenVar = JsonDocument.Parse(logIn.Content);
                        string receivedToken = receivedTokenVar.RootElement
                            .GetProperty("login")
                            .GetProperty("token")
                            .ToString();
                        Debug.WriteLine("wiadomosc po getProperty: ");
                        Debug.WriteLine(receivedToken);
                        Debug.WriteLine(receivedToken.EndsWith("\\"));
                        Debug.WriteLine(receivedToken == authToken);
                        Debug.WriteLine("login side");

                        var cookiesLogin = cookies.GetCookies(new Uri(urlPath));

                        foreach (Cookie ciacho in cookiesLogin)
                        {
                            Debug.WriteLine($"ciasteczki po login {ciacho.Value} = {ciacho.Domain}");
                        }

                    }
                    else
                    {
                        Debug.WriteLine(logIn.StatusCode);
                        Debug.WriteLine("piwo2");
                        Debug.WriteLine("login side");
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                    Debug.WriteLine("piwo3");
                    Debug.WriteLine("login side");
                }
            }
            else
            {
                Debug.WriteLine("auth token == null");
                Debug.WriteLine("login side");
            }
        }
    }
}
