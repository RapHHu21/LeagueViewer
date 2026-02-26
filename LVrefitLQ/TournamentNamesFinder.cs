
using Refit;

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
                    fields:"T.Name",
                    where:"T.TournamentLevel=\"Primary\" AND T.DateStart>=" + "\"" + dateToSend+ "\"",
                    order_by:"T.DateStart ASC",
                    join_on:""
                    );

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine(result.Content);
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
