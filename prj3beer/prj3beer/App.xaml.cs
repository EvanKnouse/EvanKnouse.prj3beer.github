using Xamarin.Forms;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;

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

            //This bit of code will be used in production, such that we will only grab sample data for debugging purposes
            //For now, we will simply load the sample data on the creation of the app instance
            //if (System.Diagnostics.Debugger.IsAttached)
            //{   // Load Fixtures for Sample Data
            //    LoadFixtures(context);
            //}
            LoadFixtures(context);

            DependencyService.Get<INotificationHandler>().Initialize();

            MainPage = new MainPage(context);
        }

        private async void LoadFixtures(BeerContext context)
        {   // Create a series of 3 new beverages with different values.
            Beverage bev1 = new Beverage { BeverageID = 1, Temperature = 2 };
            Beverage bev2 = new Beverage { BeverageID = 2, Temperature = 4 };
            Beverage bev3 = new Beverage { BeverageID = 3, Temperature = -1 };

            List<Brand> brandList = new List<Brand>();

            brandList.Add(new Brand() { brandID = 4, brandName = "Great Western Brewery" });
            brandList.Add(new Brand() { brandID = 5, brandName = "Churchhill Brewing Company" });
            brandList.Add(new Brand() { brandID = 6, brandName = "Prarie Sun Brewery" });
            brandList.Add(new Brand() { brandID = 7, brandName = new string('a', 61) });
            brandList.Add(new Brand() { brandID = 3, brandName = "" });

            ValidateBrands(brandList, context);

            Preference pref1 = new Preference { BeverageID = 1, Temperature = 10 };

            try
            {   // Try to Delete The Database
                await context.Database.EnsureDeletedAsync();
                // Try to Create the Database
                await context.Database.EnsureCreatedAsync();
                // Add Each beverage to the Database - ready to be written to the database.(watched)
                context.Beverage.Add(bev1);
                context.Beverage.Add(bev2);
                context.Beverage.Add(bev3);
                context.Preference.Add(pref1);

                // Save Changes (updates/new) to the database
                await context.SaveChangesAsync();
            }
            catch (SqliteException)
            {
                throw;
            }
        }


        /// <summary>
        /// This helper function will validate all of the brands in the brands list 
        /// that is being loaded into the app from the database fixture before saving into the database
        /// </summary>
        /// <param name="brandList"></param>
        /// <param name="bc"></param>
        private void ValidateBrands(List<Brand> brandList, BeerContext bc)
        {
            foreach (Brand brand in brandList)
            {
                if (ValidationHelper.Validate(brand).Count() == 0)               //If the validation returns 0 for count, brand is valid
                {
                    bc.Brands.Add(brand);
                }
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
