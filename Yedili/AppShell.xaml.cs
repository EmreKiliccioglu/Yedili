using Yedili.Views;

namespace Yedili
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
            nameof(GamePage),
            typeof(GamePage));
        }
    }
}
