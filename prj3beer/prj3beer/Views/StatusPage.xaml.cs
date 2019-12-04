using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prj3beer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusPage : ContentPage
    {
        public StatusPage()
        {
            InitializeComponent();
            TempReadingType(Models.Settings.TemperatureSettings);
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ToolbarItem)(sender)).IsEnabled = false;

            await Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));

            ((ToolbarItem)(sender)).IsEnabled = true;
        }

        public void UpdateTempField()
        {

        }

        public void TempReadingType(bool type)
        {
            if(type)
            {
                labelCF.Text = "\u00B0C";
            }
            else
            {
                labelCF.Text = "\u00B0F";
            }
        }

        protected override void OnAppearing()
        {
            TempReadingType(Models.Settings.TemperatureSettings);
        }

    }
}