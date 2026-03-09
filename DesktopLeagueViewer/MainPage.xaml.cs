using LVrefitLOLP;
using System.Diagnostics;

namespace DesktopLeagueViewer
{
    public partial class MainPage : ContentPage
    {
        BotLogin log;

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
