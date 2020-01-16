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
        static string beverageURL = @"http://my-json-server.typicode.com/prj3beer/prj3beer-api/beverages";
        static string brandURL = @"http://my-json-server.typicode.com/prj3beer/prj3beer-api/brands";

        public App()
        {
            InitializeComponent();

            MockTempReadings.StartCounting();

            // Instantiate a new Context (Database)
            BeerContext context = new BeerContext();

            //Instantiate a new API Manager
            ApiManager apiManager = new ApiManager();

            // Ensure the Database is Created
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //This bit of code will be used in production, such that we will only grab sample data for debugging purposes
            //For now, we will simply load the sample data on the creation of the app instance
            if (System.Diagnostics.Debugger.IsAttached)
            {   
                // Load Fixtures for Sample Data
                LoadFixtures(context, apiManager);
            }
            else
            {   
                // Load data from API
                FetchData(context, apiManager);
            }

            MainPage = new MainPage(context);
        }

        private async void FetchData(BeerContext context, ApiManager apiManager)
        {
            // Set URL of api Manager to point to the Brands API
            apiManager.BaseURL = brandURL;
            // Load the Brands that Validate into the Local Storage
            context.Brands.AddRange(await apiManager.GetBrandsAsync());

            // Set URL of api Manager to point to the Beverage API
            apiManager.BaseURL = beverageURL;
            // Load the Beverages that Validate into the Local Storage
            context.Beverage.AddRange(await apiManager.GetBeveragesAsync());

            // Save changes to the Local Storage
            await context.SaveChangesAsync();

        }

        /// <summary>
        /// This method is responsible for loading mock data into the app,
        /// this is only ran when the app had a debugger running
        /// </summary>
        /// <param name="context">Local Storage Database</param>
        /// <param name="apiManager">API Manager for handling API connection</param>
        private async void LoadFixtures(BeerContext context, ApiManager apiManager)
        {
            // Set the baseURL to an empty string
            apiManager.BaseURL = "";

            //TODO: remove this when appropriate (not testing)
            apiManager.BaseURL = brandURL;

            // Store Brands in Local Storage
            context.Brands.AddRange(await apiManager.GetBrandsAsync());

            //TODO: same as above
            apiManager.BaseURL = beverageURL;

            // Store Beverages in Local Storage
            context.Beverage.AddRange(await apiManager.GetBeveragesAsync());

            // Save the Changes
            await context.SaveChangesAsync();

            //try
            //{   // Try to Delete The Database
            //    await context.Database.EnsureDeletedAsync();

            //    // Try to Create the Database
            //    await context.Database.EnsureCreatedAsync();

            //    // Add Each beverage to the Database - ready to be written to the database.(watched)
            //    beverages.ForEach(e =>
            //    {
            //        context.Add(e);
            //    });

            //    // Save Changes (updates/new) to the database
            //    await context.SaveChangesAsync();
            //}
            //catch (SqliteException)
            //{
            //    throw;
            //}


            //// Create a series of 3 new beverages with different values
            //List<Brand> brandList = new List<Brand>();

            //brandList.Add(new Brand() { BrandID = 4, Name = "Great Western Brewery" });
            //brandList.Add(new Brand() { BrandID = 5, Name = "Churchhill Brewing Company" });
            //brandList.Add(new Brand() { BrandID = 6, Name = "Prarie Sun Brewery" });
            //brandList.Add(new Brand() { BrandID = 7, Name = new string('a', 61) });
            //brandList.Add(new Brand() { BrandID = 3, Name = "" });

            //ValidateBrands(brandList, context);

            //Beverage bev1 = new Beverage { BeverageID = 1, Name = "Great Western Radler", Brand = brandList.ElementAt(0), Type = Type.Radler,  Temperature = 2 };
            //Beverage bev2 = new Beverage { BeverageID = 2, Name = "Churchill Blonde Lager", Brand = brandList.ElementAt(1), Type = Type.Lager, Temperature = 3 };
            //Beverage bev3 = new Beverage { BeverageID = 3, Name = "Batch 88", Brand = brandList.ElementAt(2), Type = Type.Stout, Temperature = 4 };



            //Preference pref1 = new Preference { BeverageID = 1, Temperature = 10 };

            //try
            //{   // Try to Delete The Database
            //    await context.Database.EnsureDeletedAsync();
            //    // Try to Create the Database
            //    await context.Database.EnsureCreatedAsync();
            //    // Add Each beverage to the Database - ready to be written to the database.(watched)
            //    context.Beverage.Add(bev1);
            //    context.Beverage.Add(bev2);
            //    context.Beverage.Add(bev3);
            //    context.Preference.Add(pref1);

            //    // Save Changes (updates/new) to the database
            //    await context.SaveChangesAsync();
            //}
            //catch (SqliteException)
            //{
            //    throw;
            //}
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
