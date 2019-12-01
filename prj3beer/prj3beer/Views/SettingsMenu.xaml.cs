
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
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

            lblTemp.Text = e.Value ? "Celsius" : "Fahrenheit";
            
            

        }
            
    }
}