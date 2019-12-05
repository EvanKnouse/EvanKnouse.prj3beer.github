using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prj3beer.Services;
using prj3beer.Views;
using System.IO;
using Microsoft.Data.Sqlite;
using prj3beer.Models;

namespace prj3beer
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            BeerContext context = new BeerContext();
            context.Database.EnsureCreated();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                LoadFixtures(context);
            }

            //DependencyService.Register<MockDataStore>();
            MainPage = new MainPage(context);
        }

        private async void LoadFixtures(BeerContext context)
        {
            Beverage bev1 = new Beverage { BevID = 1, IdealTemp = 2 };
            Beverage bev2 = new Beverage { BevID = 2, IdealTemp = 4 };
            Beverage bev3 = new Beverage { BevID = 3, IdealTemp = -1 };

            try
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                context.Beverage.Add(bev1);
                context.Beverage.Add(bev2);
                context.Beverage.Add(bev3);

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
