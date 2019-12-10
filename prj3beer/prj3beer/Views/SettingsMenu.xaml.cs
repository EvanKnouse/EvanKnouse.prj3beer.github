
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
            //Check the Settings class to see if set to celsius or fahrenheit
            switchTemp.IsToggled = Models.Settings.TemperatureSettings;
        }

        //changes temperature display settings in response to the switch 
        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            lblTemp.Text = e.Value ? "Celsius" : "Fahrenheit";

            Models.Settings.TemperatureSettings = e.Value;
        }

        //Closes the settings modal
        async private void Close_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}