using Xamarin.Forms;
using prj3beer.Services;
using prj3beer.Views;
using prj3beer.Models;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace prj3beer
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Settings.URLSetting = default;

            MockTempReadings.StartCounting();

            // Instantiate a new Context (Database)
            BeerContext context = new BeerContext();

            //Instantiate a new API Manager
            APIManager apiManager = new APIManager();

            // Connect to the API and store Beverages/Brands in the Database
#if DEBUG
            LoadFixtures(context);
#elif RELEASE
            //Release mode breaks, but can swap these for API usage
            FetchData(context, apiManager);
#endif
            MainPage = new MainPage(context);
        }

        public static async void FetchData(BeerContext context, APIManager apiManager)
        {
            // REMOVE FOR PERSISTENT Data
            context.Database.EnsureDeleted();

            // Ensure the Database is Created
            context.Database.EnsureCreated();

            // Set URL of api Manager to point to the Brands API
            // Load the Brands that Validate into the Local Storage
            context.Brands.AddRange(await apiManager.GetBrandsAsync());

            // Set URL of api Manager to point to the Beverage API
            // Load the Beverages that Validate into the Local Storage
            context.Beverage.AddRange(await apiManager.GetBeveragesAsync());

            // Save changes to the Local Storage
            Task databaseWrite = context.SaveChangesAsync();
            databaseWrite.Wait();
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

        private async void LoadFixtures(BeerContext context)
        {
            List<Brand> brandList = new List<Brand>();

            brandList.Add(new Brand() { BrandID = 4, Name = "Great Western Brewery" });
            brandList.Add(new Brand() { BrandID = 5, Name = "Churchhill Brewing Company" });
            brandList.Add(new Brand() { BrandID = 6, Name = "Prarie Sun Brewery" });
            brandList.Add(new Brand() { BrandID = 7, Name = new string('a', 61) });
            brandList.Add(new Brand() { BrandID = 3, Name = "" });
            // Story 24 Brand, for testing
            brandList.Add(new Brand() { BrandID = 25, Name = "Coors" });

            //ValidateBrands(brandList, context);

            // Create a series of 3 new beverages with different values.
            Beverage bev1 = new Beverage { BeverageID = 1, Name = "Great Western Radler", BrandID = brandList.ElementAt(0).BrandID, Type = Type.Radler, Temperature = 2 };
            Beverage bev2 = new Beverage { BeverageID = 2, Name = "Churchill Blonde Lager", BrandID = brandList.ElementAt(0).BrandID, Type = Type.Lager, Temperature = 3 };
            Beverage bev3 = new Beverage { BeverageID = 3, Name = "Batch 88", BrandID = brandList.ElementAt(2).BrandID, Type = Type.Stout, Temperature = 4 };

            // Story 24 Beverages, for testing
            Beverage bev4 = new Beverage { BeverageID = 4, Name = "Coors Light", BrandID = brandList.ElementAt(5).BrandID, Type = Type.Light, Temperature = 3 };
            Beverage bev5 = new Beverage { BeverageID = 5, Name = "Coors Banquet", BrandID = brandList.ElementAt(5).BrandID, Type = Type.Pale, Temperature = 2 };
            Beverage bev6 = new Beverage { BeverageID = 6, Name = "Coors Edge", BrandID = brandList.ElementAt(5).BrandID, Type = Type.Radler, Temperature = 5 };

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
                // Story 24 Beverages, for testing
                context.Beverage.Add(bev4);
                context.Beverage.Add(bev5);
                context.Beverage.Add(bev6);
                context.Preference.Add(pref1);

                // Save Changes (updates/new) to the database
                await context.SaveChangesAsync();
            }
            catch (SqliteException)
            {
                throw;
            }
        }

    }
}
