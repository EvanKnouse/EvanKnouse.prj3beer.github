﻿using prj3beer.Services;
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
        //Placeholder for target temperature element, implemented in another story.
        int targetTempValue = 2;

        public StatusPage()
        { 
            InitializeComponent();
           
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ToolbarItem)(sender)).IsEnabled = false;

            await Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));

            ((ToolbarItem)(sender)).IsEnabled = true;
        }

        public void TempReadingType(bool type)
        {
            //Applies appropriate units to the temporary/placeholder target temperature element.  Target temperature implemented in another story.
            labelCFTargetTemperature.Text = "\u00B0" + (type ? "C" : "F");
        }

        protected override void OnAppearing()
        {
            TempReadingType(Models.Settings.TemperatureSettings);

            //Temporary/placeholder for target temp element.  Displays a converted/unconverted temperature as appropriate.  Target temperature mplemented in another story.
            TemperatureInput.Text = Models.Settings.TemperatureSettings ? targetTempValue + "" : (int)Temperature.CelsiusToFahrenheit(targetTempValue) + "";

        }
    }
}