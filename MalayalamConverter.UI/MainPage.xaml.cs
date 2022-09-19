using MalayalamConverter.UI.Views;

namespace MalayalamConverter.UI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void mnuAsciiConverter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UnicodeConverter());
        }

        private async void mnuTypeMalayalam_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MalayalamType());
        }
    }
}