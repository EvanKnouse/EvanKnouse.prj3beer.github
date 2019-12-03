﻿using System;
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
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            ((ToolbarItem)(sender)).IsEnabled = false;

            await Navigation.PushModalAsync(new NavigationPage(new SettingsMenu()));

            ((ToolbarItem)(sender)).IsEnabled = true;
        }

    }
}