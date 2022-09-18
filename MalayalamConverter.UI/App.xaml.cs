using Microsoft.Maui.Controls.Compatibility.Platform.UWP;

namespace MalayalamConverter.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}