
using Refit;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace LVrefitLOLP
{
    public class TournamentNamesFinder : A_RefitRequester
    {
        //class to find full name of a tournament
        //regionals = {"LCK", "LPL", "LEC", "CBLOL", "LCS" } - abbreviations of main leagues
        int thisYear = DateTime.Today.Year;
        DateTime winterSplit; //year datetime.year, rest hardcoded
        DateTime springSplit;
        DateTime summmerSplit;
        DateTime currentSplit;
        string currentSplitName;

        public TournamentNamesFinder(HttpClient CCclient, CookieHandler CCcookieHandler) : base(CCclient, CCcookieHandler)
        {
            winterSplit = new DateTime(thisYear, 1, 1);
            springSplit = new DateTime(thisYear, 4, 1);
            summmerSplit = new DateTime(thisYear, 7, 1);
        }

        public async Task GetTournamentNames()
        {
            var api = RestService.For<I_CargoMatches>(cookieClient);

            try
            {
                DateTime date = DateTime.UtcNow;
                Console.WriteLine(summmerSplit.Month);
                int thisMonth = date.Month;


                switch (thisMonth){
                    case >= 7:
                        currentSplit = summmerSplit;
                        currentSplitName = "Summer Split";
                        break;
                    case >= 4:
                        currentSplit = springSplit;
                        currentSplitName = "Spring Split";
                        break;
                    case >= 1:
                        currentSplit = winterSplit;
                        currentSplitName = "Winter Split";
                        break;
                    default:
                        currentSplit = DateTime.UtcNow;
                        break;
                }

                Console.WriteLine("current split");
                Console.WriteLine(currentSplit);

                string day_end = currentSplit.Day.ToString();
                string month_end = currentSplit.Month.ToString();
                string year_end = currentSplit.Year.ToString();


                string day_start = date.Day.ToString();
                string month_start = date.Month.ToString();
                string year_start = date.Year.ToString();



                string dateToSend_start = year_end + "-" + month_end + "-" + day_end;
                string dateToSend_end = year_start + "-" + month_start + "-" + day_start;

                var result = await api.CargoMatches(
                    tables: "Tournaments=T",
                    fields: "T.Name, T.Region, T.TournamentLevel, T.Date",
                    where:"T.TournamentLevel=\"Primary\" AND T.DateStart>=" + "\"" + dateToSend_start + "\""
                            + "and T.Date>="+"\"" + dateToSend_end + "\"",

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

                    JsonNode resultJsonWheader = JsonNode.Parse(result.Content);
                    resultJsonWheader["header"] = currentSplitName;

                    string jsonString = resultJsonWheader.ToJsonString(jsonOptions);
                    //File.WriteAllText();
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
