
using Refit;
using System.Text.Json;

namespace LVrefitLOLP
{
    public class TournamentNamesFinder : A_RefitRequester
    {
        //class to find full name of a tournament
        private string[] regionals = {"LCK", "LPL", "LEC", "CBLOL", "LCS" }; //main leagues
        private string internationals;

        public TournamentNamesFinder(HttpClient CCclient, CookieHandler CCcookieHandler) : base(CCclient, CCcookieHandler)
        {

        }

        public async Task GetTournamentNames()
        {
            var api = RestService.For<I_CargoMatches>(cookieClient);

            try
            {

                DateTime date = DateTime.UtcNow;
                DateTime currentDate = date.AddDays(-25);
                string day = currentDate.Day.ToString();
                string month = currentDate.Month.ToString();
                string year = currentDate.Year.ToString();

                string dateToSend = year + "-" + month + "-" + day;

                var result = await api.CargoMatches(
                    tables: "Tournaments=T",
                    fields: "T.Name, T.Region, T.TournamentLevel, T.Date",
                    where:"T.TournamentLevel=\"Primary\" AND T.DateStart>=" + "\"" + dateToSend+ "\"",
                    order_by:"T.DateStart ASC",
                    join_on:""
                    );

                if (result.IsSuccessStatusCode)
                {

                    var jsonResult = JsonDocument.Parse(result.Content);

                    DeserializeCargoNames? deserializedNames = JsonSerializer.Deserialize<DeserializeCargoNames>(result.Content);

                    string fileName = "CargoTournamentNames.json";
                    string baseDir = AppContext.BaseDirectory;
                    string filepath = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", "..", "LVrefitLQ", fileName));

                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented =true
                    };

                    string jsonString = JsonSerializer.Serialize(jsonResult, jsonOptions);
                    await File.WriteAllTextAsync(filepath, jsonString);


                    Console.WriteLine("Test Start");
                    foreach (var tournamentNameS in deserializedNames.cargoquery)
                    {
                        Console.WriteLine(tournamentNameS.title.Name);
                    }
                    Console.WriteLine("Test End");

                    string tournamentName = deserializedNames.cargoquery[0].title.Name;
                    Console.WriteLine(tournamentName);
                    Console.WriteLine(result.RequestMessage.RequestUri);
                    Console.WriteLine("test nazw turniejuw");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
