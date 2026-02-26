using LVrefitLOLP;
using LVrefitLQ;

namespace TestRunner
{
    class Program()
    {
        public static async Task Main(string[] args)
        {
            //Console.WriteLine("uwu");

            CookieHandler cookiesVar = new CookieHandler("https://lol.fandom.com");
            HttpClient publicClient = cookiesVar.client;

            BotLogin testowyBocik = new BotLogin(publicClient, cookiesVar);
            await testowyBocik.LogBot();

            //HarmonogramQuerryBuilder harmonogram = new HarmonogramQuerryBuilder(publicClient, cookiesVar);
            //await harmonogram.testThing();

            
            TournamentNamesFinder findName = new TournamentNamesFinder(publicClient, cookiesVar);
            await findName.GetTournamentNames();

        }
    }
}