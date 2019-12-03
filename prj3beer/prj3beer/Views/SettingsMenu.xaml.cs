
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsMenu : ContentPage
    {
        public SettingsMenu()
        {
            InitializeComponent();
            switchTemp.IsToggled = Models.Settings.TemperatureSettings;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            
            lblTemp.Text = e.Value ? "Celsius" : "Fahrenheit";

            Models.Settings.TemperatureSettings = e.Value;
        }

        async private void Close_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}