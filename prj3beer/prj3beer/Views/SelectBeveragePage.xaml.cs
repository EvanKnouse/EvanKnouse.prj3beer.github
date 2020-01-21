﻿using prj3beer.Models;
using prj3beer.Services;
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
    public partial class SelectBeveragePage : ContentPage
    {
        private List<Beverage> beverageList;
        private List<string> validBeverages;
        private APIManager apiManager;
        private BeerContext context;

        public SelectBeveragePage()
        {
            InitializeComponent();

            apiManager = new APIManager();
            context = new BeerContext();

            PopulateList();

            refresh.Clicked += (sender, e) =>
            {
                Settings.URLSetting = "http://www.google.ca";

                beverageListView.ItemsSource = "";
                beverageList = null;
                validBeverages = null;

                context.Dispose();
                context = new BeerContext();

                App.FetchData(context, apiManager);

                PopulateList();
            };
        }

        private void PopulateList()
        {
            // Initialize lists to store the beverages
            beverageList = new List<Beverage>();
            validBeverages = new List<string>();

            // Setup the context to access our stored beverages
            //context = new BeerContext();

            // Store the local beverages in the list
            beverageList = context.Beverage.ToList();

            context.SaveChanges();

            // Perform validation on the list, removing invalid beverages
            beverageList.ForEach(e => { if (ValidationHelper.Validate(e).Count == 0) { validBeverages.Add(e.Name); } });

            // Sort the list alphabetically
            validBeverages.Sort((a, b) => { return string.Compare(a, b); });

            // If there are any valid beverages,
            if (validBeverages.Count != 0)
            {   // Set the source of the List View to the valid beverages.
                beverageListView.ItemsSource = validBeverages;
            }
            // If there are no valid beverages,
            else
            {
                validBeverages.Add("Connection issue, please try again later");
                // Display an error on the screen instead of a beverage,
                beverageListView.ItemsSource = validBeverages;
                // Make sure the item is not clickable
                beverageListView.IsEnabled = false;
            }
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}