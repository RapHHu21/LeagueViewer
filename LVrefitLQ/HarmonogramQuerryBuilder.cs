using LVrefitLOLP;
using Refit;

namespace LVrefitLQ
{

  //"action": "cargoquery",
  //"format": "json",
  //"tables": "ScoreboardGames=SG, Tournaments=T",
  //"fields": "T.Name, SG.DateTime_UTC, SG.Team1, SG.Team2",
  //"where": "T.Name = 'LCK Cup 2026'",
  //"join_on": "SG.OverviewPage=T.OverviewPage",
  //"formatversion": "2"

    public class HarmonogramQuerryBuilder : A_RefitRequester
    {
        //get current time
        //get league
        //parameter what to do in constructor

        static string endpoint = "https://lol.fandom.com"; //kinda useless rn  

        public HarmonogramQuerryBuilder(HttpClient CCclient, CookieHandler CCcookieHandler) : base(CCclient, CCcookieHandler)
        {
        }

        public async Task buildQuerry()            
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

            var api = RestService.For<I_CargoMatches>(cookieClient);

            try
            {
                var result = await api.CargoMatches(
                    tables: "ScoreboardGames = SG, Tournaments = T",
                    fields: "T.Name, SG.DateTime_UTC",
                    join_on: "SG.OverviewPage=T.OverviewPage",
                    where: null
                );

                Console.WriteLine(result.RequestMessage.RequestUri);

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine(result.Content);

                    cookieHandler.ShowCookiesMSG("TestQuerry");
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
    }
}
