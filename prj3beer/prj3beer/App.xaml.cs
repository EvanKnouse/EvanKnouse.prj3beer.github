using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using Microsoft.Data.Sqlite;

namespace prj3beer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            MockTempReadings.StartCounting();

            // Instantiate a new Context (Database)
            BeerContext context = new BeerContext();
            // Ensure the Database is Created
            context.Database.EnsureCreated();

            if (System.Diagnostics.Debugger.IsAttached)
            {   // Load Fixtures for Sample Data
                LoadFixtures(context);
            }

            MainPage = new MainPage();
        }
        private async void LoadFixtures(BeerContext context)
        {   // Create a series of 3 new beverages with different values.
            Beverage bev1 = new Beverage { BeverageID = 1, Temperature = 2 };
            Beverage bev2 = new Beverage { BeverageID = 2, Temperature = 4 };
            Beverage bev3 = new Beverage { BeverageID = 3, Temperature = -1 };

            try
            {   // Try to Delete The Database
                await context.Database.EnsureDeletedAsync();
                // Try to Create the Database
                await context.Database.EnsureCreatedAsync();
                // Add Each beverage to the Database - ready to be written to the database.(watched)
                context.Beverage.Add(bev1);
                context.Beverage.Add(bev2);
                context.Beverage.Add(bev3);

                // Save Changes (updates/new) to the database
                await context.SaveChangesAsync();
            }
            catch (SqliteException)
            {
                throw;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
