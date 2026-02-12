using LVrefitLOLP;
using LVrefitLQ;

namespace TestRunner
{
    class Program()
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("uwu");
            

            BotLogin testowyBocik = new BotLogin("RapHHu21@LeagueViewerBot", "08nmqt8anhrdajveajtrdk2uakd7qo5q");
            await testowyBocik.LogBot();
        }
    }
}