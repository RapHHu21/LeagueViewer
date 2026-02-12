using LVrefitLOLP;
using Refit;
using System.Threading.Tasks;

namespace LVrefitLQ
{
  //  
  //"action": "cargoquery",
  //"format": "json",
  //"tables": "ScoreboardGames=SG, Tournaments=T",
  //"fields": "T.Name, SG.DateTime_UTC, SG.Team1, SG.Team2",
  //"where": "T.Name = 'LCK Cup 2026'",
  //"join_on": "SG.OverviewPage=T.OverviewPage",
  //"formatversion": "2"
  //  

    public class HarmonogramQuerryBuilder
    {
        //get current time
        //get league
        //parameter what to do in constructor

        static string endpoint = "https://lol.fandom.com";
        //    "https://lol.fandom.com/api.php?action=cargoquery&format=json&" +
        //    "limit=50&tables=ScoreboardGames%20%3D%20SG%2C%20Tournaments%20%3D%20T&" +
        //    "fields=T.Name%2C%20SG.DateTime_UTC%2C%20SG.Team1%2C%20SG.Team2&" +
        //    "where=T.Name%20%3D%20'LCK%20Cup%202026'&" +
        //    "join_on=SG.OverviewPage%20%3D%20T.OverviewPage&formatversion=2";
        ////rn its a whole querry just for the example of how it looks
        

        public HarmonogramQuerryBuilder()
        {

        }

        private struct buildQuerry(string leagueRegion)
        {
            string datetime = getDateTime();            
            
        }

        public static string getDateTime()
        {
            string currTime = DateTime.UtcNow.ToString();
            return currTime;
        }

        public async Task testThing()
        {

            var api = RestService.For<I_CargoQueryTest>("https://lol.fandom.com");
            try
            {
                var result = await api.CargoQueryTests(
                    tables: "ScoreboardGames = SG, Tournaments = T",
                    fields: "T.Name, SG.DateTime_UTC",
                    join_on: "SG.OverviewPage=T.OverviewPage"
                );

                Console.WriteLine(result.RequestMessage.RequestUri);

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine(result.Content);
                }
                else
                {
                    Console.WriteLine(result.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task testWiki2()
        {
            var api = RestService.For<ItestWiki>("http://en.wikipedia.org/w");
            try
            {
                var res = await api.WikiQueryAsync(
                    action: "query",
                    list: "search",
                    srSearch: "Craig Noone",
                    format: "json"
                );


                if (!res.IsSuccessStatusCode)
                {
                    Console.WriteLine(res.Content);
                    Console.WriteLine("wypadek1");
                }
                else
                {
                    Console.WriteLine(res.StatusCode);
                    Console.WriteLine("wypadek2");
                }



            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("wypadek3");
            }
        }

    }

    public interface ItestWiki
    {
        [Get("/api.php")]
        Task<ApiResponse<string>> WikiQueryAsync(
            [AliasAs("action")] string action,
            [AliasAs("list")] string list,
            [AliasAs("srsearch")] string srSearch,
            [AliasAs("format")] string format
            );
    }

}
