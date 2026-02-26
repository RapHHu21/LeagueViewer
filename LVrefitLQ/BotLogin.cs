using Refit;
using System.Diagnostics;
using System.Text.Json;

namespace LVrefitLOLP
{
    public class BotLogin : A_RefitRequester
    {
        //login problem solved, encoding url instead of json in post
        private string _username;
        private string _password;
        private string? authToken;
        private string urlPath = "https://lol.fandom.com";
        private string fileName = "botCreds.json";

        public BotLogin(HttpClient CCclient, CookieHandler CCcookieHandler) : base(CCclient, CCcookieHandler)
        {
            readJsonCreds();
        }

        private void readJsonCreds()
        {
            string baseDir = AppContext.BaseDirectory;
            string filePath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..","..", "LVrefitLQ", fileName));
            string readFromFile = File.ReadAllText(filePath);
            DeserializePassword? passwordFile = JsonSerializer.Deserialize<DeserializePassword>(readFromFile);

            _username = passwordFile.login;
            _password = passwordFile.password;
        }

        public async Task GetToken(HttpClient cookieUrl)
        {           

            var api = RestService.For<I_LoginToken>(cookieClient);
            
            try
            {
                var getToken = await api.LoginToken();

                Debug.WriteLine("url:" + getToken.RequestMessage.RequestUri);

                if (getToken.IsSuccessStatusCode)
                {                   
                    Debug.WriteLine(getToken.Content);
                    var jsonResponse = getToken.Content;
                    var parseJson = JsonDocument.Parse(jsonResponse);
                    
                    string? origToken = parseJson.RootElement
                        .GetProperty("query")
                        .GetProperty("tokens")
                        .GetProperty("logintoken")
                        .GetString();
                    
                    authToken = origToken;
                    Debug.WriteLine(authToken);
                }
                else
                {
                    Debug.WriteLine(getToken.StatusCode);
                    Debug.WriteLine(getToken.Error);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task LogBot()
        {         

            await GetToken(cookieClient);           

            cookieHandler.ShowCookiesMSG("GetToken");


            if(authToken != null)
            {

                var api = RestService.For<I_BotLogin>(cookieClient);

                try
                {
                    var botLogin = _username;                    
                    var botParams = new BotLoginParams
                    {
                        lgpassword = _password,
                        lgtoken = authToken,
                    };
                    
                    var logIn = await api.BotLoginPost(botParams, botLogin);

                    Debug.WriteLine("url:" + logIn.RequestMessage.RequestUri);

                    if (logIn.IsSuccessStatusCode)
                    {
                        Debug.WriteLine(logIn.Content);
                        var receivedFile = JsonDocument.Parse(logIn.Content);

                        cookieHandler.ShowCookiesMSG("LogIn");

                        //if login failed with 'NeedToken error'
                        if(receivedFile.RootElement.GetProperty("login").GetProperty("result").ToString() != "Success")
                        {
                            var receivedTokenVar = JsonDocument.Parse(logIn.Content);
                            string receivedToken = receivedTokenVar.RootElement
                                .GetProperty("login")
                                .GetProperty("token")
                                .ToString();
                            Debug.WriteLine(receivedToken);
                        }
                    }
                    else
                    {
                        Debug.WriteLine(logIn.StatusCode);
                    }
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            else
            {
                Debug.WriteLine("auth token == null");
            }
        }
    }
}
