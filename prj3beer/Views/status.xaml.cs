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
    public partial class status : ContentPage
    {
        public status()
        {
            InitializeComponent();

            Temperature temperature = new Temperature();

            string[] array = { "1", "2" };

            currentTemp.Text = temperature.TemperatureCheck(array);

        }
    }
}