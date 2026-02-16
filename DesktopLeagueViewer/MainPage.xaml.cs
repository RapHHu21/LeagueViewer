using LVrefitLOLP;
using System.Diagnostics;

namespace DesktopLeagueViewer
{
    public partial class MainPage : ContentPage
    {
        BotLogin logBot = new BotLogin("RapHHu21@LeagueViewerBot", "08nmqt8anhrdajveajtrdk2uakd7qo5q");

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }        

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
            Debug.WriteLine(count);
        }

        private async void LogInBot(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine("test");
                await logBot.LogBot();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        

    }
}
